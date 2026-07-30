//---------------------------------------------------------
// Global Variables
//---------------------------------------------------------

let selectedShop = null;

let selectedItems = [];

let shopPriceList = [];


//---------------------------------------------------------
// DOM Loaded
//---------------------------------------------------------

document.addEventListener("DOMContentLoaded", () => {

    registerEvents();

});


//---------------------------------------------------------
// Register Events
//---------------------------------------------------------


function registerEvents() {

    document.addEventListener("click", function (e) {

        if (e.target.id === "btnBrowseShops") {

            loadNearbyShops();

        }

    });

}
document.addEventListener("click", function (e) {

    //-----------------------------------------
    // View Price List
    //-----------------------------------------

    if (e.target.classList.contains("btnPriceList")) {

        const shopId =
            e.target.dataset.shopid;

        loadPriceList(shopId);

        return;

    }

    //-----------------------------------------
    // Select Shop
    //-----------------------------------------

    if (e.target.classList.contains("btnSelectShop")) {

        selectShop({

            id: e.target.dataset.shopid,

            shopName: e.target.dataset.shopname,

            city: e.target.dataset.city,

            state: e.target.dataset.state,

            image: e.target.dataset.image

        });

        return;

    }
    //----------------------------------
    // Add Cloth
    //----------------------------------

    if (e.target.classList.contains("btnAddCloth")) {

        addCloth(

            Number(e.target.dataset.id),

            e.target.dataset.name,

            Number(e.target.dataset.price)

        );

    }

});


//---------------------------------------------------------
// Browse Shops
//---------------------------------------------------------

async function loadNearbyShops() {

    const container =
        document.getElementById("shopContainer");

    container.innerHTML = `

        <div class="text-center p-5">

            Loading nearby shops...

        </div>

    `;

    try {

        const response =
            await fetch("/Customer/Orders/GetNearbyShops");

        if (!response.ok) {

            throw new Error("Unable to load nearby shops.");

        }

        const shops =
            await response.json();

        renderShopCards(shops);

    }
    catch (error) {

        container.innerHTML = `

            <div class="alert alert-danger">

                ${error.message}

            </div>

        `;

    }

}//---------------------------------------------------------
// Add Cloth
//---------------------------------------------------------

function addCloth(id, name, price) {

    const existing =
        selectedItems.find(x => x.clothTypeId === id);

    if (existing) {

        existing.quantity++;

    }
    else {

        selectedItems.push({

            clothTypeId: id,

            clothName: name,

            price: price,

            quantity: 1

        });

    }
    console.log(selectedItems);

    renderSelectedItems();

    calculateTotal();

}

//---------------------------------------------------------
// Render Shop Cards
//---------------------------------------------------------


function renderShopCards(shops) {

    const container =
        document.getElementById("shopContainer");

    container.innerHTML = "";

    if (!shops || shops.length === 0) {

        container.innerHTML = `

            <div class="empty-box">

                No Nearby Shops Found

            </div>

        `;

        return;

    }

    shops.forEach(shop => {

        const card = `

<div class="shop-card">

    <img
        class="shop-image"
        src="${shop.shopImageUrl ?? "/images/no-image.png"}" />

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

                Delivery ${shop.estimatedDeliveryMinutes} mins

            </span>

            <span>

                ₹${shop.minimumOrderAmount}

            </span>

            <span>

                ${shop.isOpen ? "🟢 Open" : "🔴 Closed"}

            </span>

        </div>

        <div class="shop-actions">

            <button
                type="button"
                class="btn btn-outline-primary btnPriceList"
                data-shopid="${shop.shopOwnerId}">

                View Price List

            </button>

            <button
                type="button"
                class="btn btn-success btnSelectShop"

                data-shopid="${shop.shopOwnerId}"

                data-shopname="${shop.shopName}"

                data-city="${shop.city}"

                data-state="${shop.state}"

                data-image="${shop.shopImageUrl ?? ""}">

                Select Shop

            </button>

        </div>

    </div>

</div>

`;

        container.insertAdjacentHTML("beforeend", card);

    });

}

//---------------------------------------------------------
// View Price List
//---------------------------------------------------------

async function loadPriceList(shopOwnerId) {

    try {

        const response =
            await fetch(`/Customer/Orders/GetPriceList?shopOwnerId=${shopOwnerId}`);

        if (!response.ok) {

            throw new Error("Unable to load price list.");

        }

        const result = await response.json();

        shopPriceList = result.data;

        let html = `

<table class="table table-bordered table-hover">

    <thead>

        <tr>

            <th>Cloth</th>

            <th>Price</th>

            <th width="120">Action</th>

        </tr>

    </thead>

    <tbody>

`;

        shopPriceList.forEach(item => {

            html += `

<tr>

    <td>${item.clothName}</td>

    <td>₹${item.price}</td>

    <td>

        <button
            type="button"
            class="btn btn-success btn-sm btnAddCloth"
            data-id="${item.clothTypeId}"
            data-name="${item.clothName}"
            data-price="${item.price}">

            Add

        </button>

    </td>

</tr>

`;

        });

        html += `

    </tbody>

</table>

`;

        document.getElementById("priceListContainer").innerHTML = html;

        const modal =
            new bootstrap.Modal(document.getElementById("priceListModal"));

        modal.show();

    }
    catch (error) {

        alert(error.message);

    }

}

//---------------------------------------------------------
// Select Shop
//---------------------------------------------------------

function selectShop(shop) {

    selectedShop = shop;

    document.getElementById("SelectedShopOwnerId").value =
        shop.id;

    document.getElementById("selectedShopCard").innerHTML = `

<div class="selected-shop-info">

    <img
        src="${shop.image || "/images/no-image.png"}"
        class="selected-shop-image" />

    <div>

        <h4>${shop.shopName}</h4>

        <p>${shop.city}, ${shop.state}</p>

        <span class="badge bg-success">

            Shop Selected

        </span>

    </div>

</div>

<div class="mt-3">

    <button
        type="button"
        id="btnBrowseShops"
        class="btn btn-outline-primary">

        Change Shop

    </button>

</div>

`;

    document.querySelector(".shop-grid").style.display = "none";
    loadAvailableClothes(shop.id);
}
function renderAvailableClothes() {

    const container =
        document.getElementById("availableClothesContainer");

    container.innerHTML = "";

    shopPriceList.forEach(item => {

        const html = `

<div class="cloth-card">

    <h5>${item.clothName}</h5>

    <h4>₹${item.price}</h4>

    <button
        type="button"
        class="btn btn-success btnAddCloth"
        data-id="${item.clothTypeId}"
        data-name="${item.clothName}"
        data-price="${item.price}">

        Add

    </button>

</div>

`;

        container.insertAdjacentHTML("beforeend", html);

    });

}
//---------------------------------------------------------
// Load Available Clothes
//---------------------------------------------------------

async function loadAvailableClothes(shopId) {

    const container =
        document.getElementById("availableClothesContainer");

    document.getElementById("availableClothesSection").style.display =
        "block";

    container.innerHTML =
        "<div class='text-center p-3'>Loading...</div>";

    try {

        const response =
            await fetch(`/Customer/Orders/GetPriceList?shopOwnerId=${shopId}`);

        if (!response.ok)
            throw new Error("Unable to load clothes.");

        const result =
            await response.json();

        shopPriceList = result.data;

        renderAvailableClothes();

    }
    catch (error) {

        container.innerHTML =
            `<div class="alert alert-danger">${error.message}</div>`;

    }

}


//---------------------------------------------------------
// Render Selected Clothes
//---------------------------------------------------------

function renderSelectedItems() {

    const container =
        document.getElementById("selectedItemsContainer");

    if (selectedItems.length === 0) {

        container.innerHTML = `

<div class="empty-box">

No clothes selected.

</div>

`;

        return;

    }

    let html = `

<table class="table table-bordered">

<thead>

<tr>

<th>Cloth</th>

<th>Price</th>

<th>Qty</th>

<th>Total</th>

</tr>

</thead>

<tbody>

`;

    selectedItems.forEach(item => {

        html += `

<tr>

<td>${item.clothName}</td>

<td>₹${item.price}</td>

<td>${item.quantity}</td>

<td>₹${item.price * item.quantity}</td>

</tr>

`;

    });

    html += `

</tbody>

</table>

`;

    container.innerHTML = html;

}

//---------------------------------------------------------
// Calculate Total
//---------------------------------------------------------

function calculateTotal() {

    let total = 0;

    selectedItems.forEach(item => {

        total += item.price * item.quantity;

    });

    document.getElementById("totalAmount").innerText =
        `₹${total}`;

}


//---------------------------------------------------------
// Create Order
//---------------------------------------------------------

async function createOrder() {

}


