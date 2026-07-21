document.addEventListener("DOMContentLoaded", () => {

    const browseButton = document.getElementById("btnBrowseShops");

    if (browseButton) {

        browseButton.addEventListener("click", loadNearbyShops);

    }

});


// ===============================
// Browse Nearby Shops
// ===============================

async function loadNearbyShops() {

    const shopContainer =
        document.getElementById("shopContainer");

    shopContainer.innerHTML =
        "<div class='text-center p-5'>Loading nearby shops...</div>";

    try {

        const response =
            await fetch("/Customer/Orders/GetNearbyShops");

        if (!response.ok) {

            throw new Error("Unable to load nearby shops.");

        }

        const result =
            await response.json();

        renderShopCards(result);

    }
    catch (error) {

        shopContainer.innerHTML =

            `<div class="alert alert-danger">

                ${error.message}

            </div>`;

    }

}


// ===============================
// Render Shop Cards
// ===============================

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
         src="${shop.shopImageUrl ?? "/images/no-image.png"}" />

    <div class="shop-body">

        <div class="shop-name">

            ${shop.shopName}

        </div>

        <div class="shop-location">

            ${shop.city}, ${shop.state}

        </div>

        <div class="shop-description">

            ${shop.description ?? ""}

        </div>

        <div class="shop-info">

            <span>

                Delivery ${shop.estimatedDeliveryMinutes} mins

            </span>

            <span>

                Minimum ₹${shop.minimumOrderAmount}

            </span>

            <span>

                ${shop.isOpen ? "🟢 Open" : "🔴 Closed"}

            </span>

        </div>

        <div class="shop-actions">

            <button
                type="button"
                class="btn btn-outline-primary btnPriceList"
                data-shop="${shop.shopOwnerId}">

                View Price List

            </button>

            <button
                type="button"
                class="btn btn-success btnSelectShop"
                data-shop="${shop.shopOwnerId}"
                data-shopname="${shop.shopName}">

                Select Shop

            </button>

        </div>

    </div>

</div>

`;

        container.insertAdjacentHTML("beforeend", card);

    });

}


// ===============================
// Click Events
// ===============================

document.addEventListener("click", async function (e) {

    //----------------------------------------------------
    // View Price List
    //----------------------------------------------------

    if (e.target.classList.contains("btnPriceList")) {

        const shopId =
            e.target.dataset.shop;

        try {

            const response =
                await fetch(`/Customer/Orders/GetPriceList?shopOwnerId=${shopId}`);

            if (!response.ok) {

                throw new Error("Unable to load price list.");

            }

            const result =
                await response.json();

            renderPriceList(result.data);

            const modal =
                new bootstrap.Modal(
                    document.getElementById("priceListModal"));

            modal.show();

        }
        catch (error) {

            alert(error.message);

        }

    }


    //----------------------------------------------------
    // Select Shop
    //----------------------------------------------------

    if (e.target.classList.contains("btnSelectShop")) {

        const shopId =
            e.target.dataset.shop;

        const shopName =
            e.target.dataset.shopname;

        document.getElementById("SelectedShopOwnerId").value =
            shopId;

        document.getElementById("selectedShopCard").innerHTML =

            `
            <h5>${shopName}</h5>

            <p>

                Shop Selected Successfully

            </p>

            <button
                type="button"
                id="btnBrowseShops"
                class="btn btn-outline-primary">

                Change Shop

            </button>
            `;

        const modalElement =
            document.getElementById("priceListModal");

        const modal =
            bootstrap.Modal.getInstance(modalElement);

        if (modal) {

            modal.hide();

        }

    }

});


// ===============================
// Render Price List
// ===============================

function renderPriceList(items) {

    let html = "";

    html += `

<table class="table table-bordered table-hover">

    <thead>

        <tr>

            <th>Cloth</th>

            <th>Price</th>

        </tr>

    </thead>

    <tbody>

`;

    items.forEach(item => {

        html += `

<tr>

    <td>

        ${item.clothName}

    </td>

    <td>

        ₹${item.price}

    </td>

</tr>

`;

    });

    html += `

    </tbody>

</table>

`;

    document.getElementById("priceListContainer").innerHTML =
        html;

}