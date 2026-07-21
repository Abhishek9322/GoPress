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

}

//---------------------------------------------------------
// Render Selected Clothes
//---------------------------------------------------------

function renderSelectedItems() {

}


//---------------------------------------------------------
// Calculate Total
//---------------------------------------------------------

function calculateTotal() {

}


//---------------------------------------------------------
// Create Order
//---------------------------------------------------------

async function createOrder() {

}




////---------------------------------------------------
//// Selected Order Items
////---------------------------------------------------

//let selectedItems = [];

//document.addEventListener("DOMContentLoaded", () => {

//    const browseButton = document.getElementById("btnBrowseShops");

//    if (browseButton) {

//        browseButton.addEventListener("click", loadNearbyShops);

//    }

//});


//// ===============================
//// Browse Nearby Shops
//// ===============================

//async function loadNearbyShops() {

//    const shopContainer =
//        document.getElementById("shopContainer");

//    shopContainer.innerHTML =
//        "<div class='text-center p-5'>Loading nearby shops...</div>";

//    try {

//        const response =
//            await fetch("/Customer/Orders/GetNearbyShops");

//        if (!response.ok) {

//            throw new Error("Unable to load nearby shops.");

//        }

//        const result =
//            await response.json();

//        renderShopCards(result);

//    }
//    catch (error) {

//        shopContainer.innerHTML =

//            `<div class="alert alert-danger">

//                ${error.message}

//            </div>`;

//    }

//}


//// ===============================
//// Render Shop Cards
//// ===============================

//function renderShopCards(shops) {

//    const container =
//        document.getElementById("shopContainer");

//    container.innerHTML = "";

//    if (!shops || shops.length === 0) {

//        container.innerHTML =

//            `<div class="empty-box">

//                No Nearby Shops Found

//            </div>`;

//        return;

//    }

//   items.forEach(item => {

//    html += `

//<tr>

//    <td>${item.clothName}</td>

//    <td>₹${item.price}</td>

//    <td>

//        <div class="qty-box">

//            <button
//                type="button"
//                class="btn btn-sm btn-outline-danger btnMinus"
//                data-id="${item.clothTypeId}"
//                data-name="${item.clothName}"
//                data-price="${item.price}">

//                -

//            </button>

//            <span
//                id="qty-${item.clothTypeId}"
//                class="mx-3">

//                0

//            </span>

//            <button
//                type="button"
//                class="btn btn-sm btn-outline-success btnPlus"
//                data-id="${item.clothTypeId}"
//                data-name="${item.clothName}"
//                data-price="${item.price}">

//                +

//            </button>

//        </div>

//    </td>

//</tr>

//`;



//        container.insertAdjacentHTML("beforeend", card);

//    });

//}


//// ===============================
//// Click Events
//// ===============================

//document.addEventListener("click", async function (e) {

//    //----------------------------------------------------
//    // View Price List
//    //----------------------------------------------------

//    if (e.target.classList.contains("btnPriceList")) {

//        const shopId =
//            e.target.dataset.shop;

//        try {

//            const response =
//                await fetch(`/Customer/Orders/GetPriceList?shopOwnerId=${shopId}`);

//            if (!response.ok) {

//                throw new Error("Unable to load price list.");

//            }

//            const result =
//                await response.json();

//            renderPriceList(result.data);

//            const modal =
//                new bootstrap.Modal(
//                    document.getElementById("priceListModal"));

//            modal.show();

//        }
//        catch (error) {

//            alert(error.message);

//        }

//    }
//    if (e.target.classList.contains("btnPlus")) {

//        const id = Number(e.target.dataset.id);

//        const clothName = e.target.dataset.name;

//        const price = Number(e.target.dataset.price);

//        const qtyElement =
//            document.getElementById(`qty-${id}`);

//        let qty = Number(qtyElement.innerText);

//        qty++;

//        qtyElement.innerText = qty;

//        updateOrderItem(id, clothName, price, qty);

//        return;

//    }
//    if (e.target.classList.contains("btnMinus")) {

//        const id = Number(e.target.dataset.id);

//        const clothName = e.target.dataset.name;

//        const price = Number(e.target.dataset.price);

//        const qtyElement =
//            document.getElementById(`qty-${id}`);

//        let qty = Number(qtyElement.innerText);

//        if (qty > 0) {

//            qty--;

//            qtyElement.innerText = qty;

//            updateOrderItem(id, clothName, price, qty);

//        }

//        return;

//    }
//    //----------------------------------------------------
//    // Increase Quantity
//    //----------------------------------------------------

//    if (e.target.classList.contains("btnIncrease")) {

//        const card =
//            e.target.closest(".cloth-card");

//        const qty =
//            card.querySelector(".quantity");

//        qty.innerText =
//            parseInt(qty.innerText) + 1;

//    }



//    //----------------------------------------------------
//    // Decrease Quantity
//    //----------------------------------------------------

//    if (e.target.classList.contains("btnDecrease")) {

//        const card =
//            e.target.closest(".cloth-card");

//        const qty =
//            card.querySelector(".quantity");

//        let value =
//            parseInt(qty.innerText);

//        if (value > 0)
//            qty.innerText = value - 1;

//    }

//    //----------------------------------------------------
//    // Select Shop
//    //----------------------------------------------------

//    if (e.target.classList.contains("btnSelectShop")) {

//        const shopId =
//            e.target.dataset.shop;

//        const shopName =
//            e.target.dataset.shopname;

//        document.getElementById("SelectedShopOwnerId").value =
//            shopId;

//        document.getElementById("selectedShopCard").innerHTML =

//            `
//            <h5>${shopName}</h5>

//            <p>

//                Shop Selected Successfully

//            </p>

//            <button
//                type="button"
//                id="btnBrowseShops"
//                class="btn btn-outline-primary">

//                Change Shop

//            </button>
//            `;

//        const modalElement =
//            document.getElementById("priceListModal");

//        const modal =
//            bootstrap.Modal.getInstance(modalElement);

//        if (modal) {

//            modal.hide();

//        }

//    }

//});


//// ===============================
//// Render Price List
//// ===============================

//function renderPriceList(items) {

//    const container =
//        document.getElementById("priceListContainer");

//    container.innerHTML = "";

//    if (!items || items.length === 0) {

//        container.innerHTML = `
//            <div class="alert alert-warning">
//                No price list found.
//            </div>
//        `;

//        return;
//    }

//    items.forEach(item => {

//        const card = `

//<div class="cloth-card"
//     data-clothtype="${item.clothTypeId}"
//     data-price="${item.price}">

//    <div class="cloth-name">

//        ${item.clothName}

//    </div>

//    <div class="cloth-price">

//        ₹${item.price}

//    </div>

//    <div class="quantity-box">

//        <button
//            type="button"
//            class="btn btn-outline-danger btnDecrease">

//            -

//        </button>

//        <span class="quantity">

//            0

//        </span>

//        <button
//            type="button"
//            class="btn btn-outline-success btnIncrease">

//            +

//        </button>

//    </div>

//</div>

//`;

//        container.insertAdjacentHTML("beforeend", card);

//    });

//}

//function updateOrderItem(clothTypeId, clothName, price, quantity) {

//    const existing =
//        selectedItems.find(x => x.clothTypeId == clothTypeId);

//    if (existing) {

//        existing.quantity = quantity;

//        if (quantity <= 0) {

//            selectedItems =
//                selectedItems.filter(x => x.clothTypeId != clothTypeId);

//        }

//    }
//    else {

//        if (quantity > 0) {

//            selectedItems.push({

//                clothTypeId: clothTypeId,

//                clothName: clothName,

//                price: price,

//                quantity: quantity

//            });

//        }

//    }

//    calculateTotal();

//}
//function calculateTotal() {

//    let total = 0;

//    selectedItems.forEach(item => {

//        total += item.price * item.quantity;

//    });

//    document.getElementById("totalAmount").innerText =
//        `₹${total.toFixed(2)}`;

//    document.getElementById("btnCreateOrder").disabled =
//        selectedItems.length === 0;

//}


