document.addEventListener("DOMContentLoaded", () => {

    const browseButton = document.getElementById("btnBrowseShops");

    if (browseButton) {

        browseButton.addEventListener("click", loadNearbyShops);

    }

});

async function loadNearbyShops() {

    const shopContainer =
        document.getElementById("shopContainer");

    shopContainer.innerHTML =
        "<div class='text-center p-5'>Loading nearby shops...</div>";

    try {

        const response =
            await fetch("/Customer/Orders/GetNearbyShops");

        if (!response.ok) {

            throw new Error("Unable to load shops.");

        }

        const result = await response.json();

        renderShopCards(result);

    }
    catch (error) {

        shopContainer.innerHTML =

            `<div class="alert alert-danger">

                    ${error.message}

             </div>`;

    }

}

function renderShopCards(shops) {

    const container =
        document.getElementById("shopContainer");

    container.innerHTML = "";

    if (!shops || shops.length === 0) {

        container.innerHTML =

            `<div class="empty-box">

                    No Nearby Shops Found

             </div>`;

        return;

    }

    shops.forEach(shop => {

        const card = `

<div class="shop-card">

    <img class="shop-image"
         src="${shop.shopImageUrl ?? '/images/no-image.png'}" />

    <div class="shop-body">

        <div class="shop-name">

            ${shop.shopName}

        </div>

        <div class="shop-location">

            ${shop.city},
            ${shop.state}

        </div>

        <div class="shop-description">

            ${shop.description ?? ""}

        </div>

        <div class="shop-info">

            <span>

                Delivery

                ${shop.estimatedDeliveryMinutes} mins

            </span>

            <span>

                Minimum

                ₹${shop.minimumOrderAmount}

            </span>

            <span>

                ${shop.isOpen ? "🟢 Open" : "🔴 Closed"}

            </span>

        </div>

        <div class="shop-actions">

            <button
                class="btn btn-outline-primary btnPriceList"
                data-shop="${shop.shopOwnerId}">

                View Price List

            </button>

            <button
                class="btn btn-success btnSelectShop"
                data-shop="${shop.shopOwnerId}">

                Select Shop

            </button>

        </div>

    </div>

</div>

`;

        container.insertAdjacentHTML("beforeend", card);

    });

}