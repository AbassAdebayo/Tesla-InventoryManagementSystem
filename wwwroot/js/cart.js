let cartConfig = {
    cartKey: 'carts',
    cartItemsId: 'cartItems',
    cartItemsTableId: 'cartItemsTable'
}

function getCart() {
    const cart = localStorage.getItem(cartConfig.cartKey) || "[]";
    const object = JSON.parse(cart);
    return new Map(object);
}

function addToCart(cartItem) {
    let cart = getCart();
    if (cart.has(cartItem.id)) {
        const oldCartItem = cart.get(cartItem.id);
        cartItem.quantity += oldCartItem.quantity;
        cart.set(cartItem.id, cartItem);
    }
    else
    {
        cart.set(cartItem.id, cartItem);
    }

    const cartString = JSON.stringify(Array.from(cart));
    localStorage.setItem(cartConfig.cartKey, cartString);
}

function updateQuantity(id, quantity)
{
    let cart = getCart();
    if (cart.has(id)) {
        const cartItem = cart.get(id);
        cartItem.quantity = quantity;
        cart.set(id, cartItem);
    }
    const cartString = JSON.stringify(Array.from(cart));
    localStorage.setItem(cartConfig.cartKey, cartString);
}

function clearCart() {
    let cart = getCart();
    cart.clear();
    const cartString = JSON.stringify(Array.from(cart));
    localStorage.setItem(cartConfig.cartKey, cartString);
}

function removeCartItem(id) {
    let cart = getCart();
    cart.delete(id);
    const cartString = JSON.stringify(Array.from(cart));
    localStorage.setItem(cartConfig.cartKey, cartString);
}

function getCartInfo() {
    let cart = getCart();
    let totalAmount = 0;
    let totalItemsCount = 0;
    for (const [key, value] of cart) {
        totalAmount += value.quantity * value.price;
        totalItemsCount += value.quantity;
    }
    return { totalAmount, totalItemsCount };
}


function bindCartItems(elementId) {
    let content = ``;
    let cartItems = getCart();
    for (const [key, value] of cartItems) {

        var detailsUrl = `${appConfig.appBaseUrl}/products/details/${key}`;
        content += `<li>
                                    <div class="shopping-cart-img">
                                        <a href="${detailsUrl}"><img alt="" src="${appConfig.imageBaseUrl}/assets/images/cart/cart-1.jpg"></a>
                                    </div>
                                    <div class="shopping-cart-title">
                                        <h4><a href="${detailsUrl}">${value.name}</a></h4>
                                        <h3><span>${value.quantity} × </span>₦${value.price}</h3>
                                    </div>
                                    <div class="shopping-cart-delete">
                                        <a onclick="handleRemoveFromCart('${key}')" href="javascript:void(0);"><i class="far fa-times"></i></a>
                                    </div>
                                </li>`;
    }
    document.getElementById(elementId).innerHTML = content;
}

function updateCartInfoDisplay(elementId) {
    const cartInfo = getCartInfo();
    let checkoutLink = $('#checkoutLink');
    let itemCount = $('#itemCount');
    let cartTotal = $('#cartTotal');
    cartTotal.html(`₦${cartInfo.totalAmount}`);
    itemCount.html(`${cartInfo.totalItemsCount}`);
    if (cartInfo.totalItemsCount <= 0) {
        checkoutLink.attr('disabled', 'disabled');
    }
    else {
        checkoutLink.removeAttr('disabled');
    }
    bindCartItems(elementId);
}

function bindCartItemsPage(elementId) {
    let content = ``;
    let cartItems = getCart();
    for (const [key, value] of cartItems) {
        const subtotal = value.price * value.quantity;
        var detailsUrl = `${appConfig.appBaseUrl}/products/details/${key}`;
        content += `   <tr>
                        <td class="product-thumbnail">
                            <a href="${detailsUrl}"><img src="${appConfig.imageBaseUrl}/assets/images/cart/cart-1.jpg" alt=""></a>
                        </td>
                        <td class="product-name">
                            <h5><a href="${detailsUrl}">${value.name}</a></h5>
                        </td>
                        <td class="product-price"><span class="amount">₦${value.price}</span></td>
                        <td class="cart-quality">
                            <div class="product-quality">
                                <input class="cart-plus-minus-box input-text qty text" name="qtybutton" data-id="${key}"  value="${value.quantity}">
                            </div>
                        </td>
                        <td class="product-total"><span>₦${subtotal}</span></td>
                        <td class="product-remove"><a onclick="handleRemoveFromCartPage('${key}')" href="javascript:void(0)">Remove</a></td>
                    </tr>`;
    }
    document.getElementById(elementId).innerHTML = content;
    handleQuantityChange();
}

function handleQuantityChange() {
    $(".product-quality").append('<div class="dec qtybutton">-</i></div><div class="inc qtybutton">+</div>');
    $(".qtybutton").on("click", function () {
        let $button = $(this);
        let quantity = $button.parent().find("input").val();
        const id = $('.cart-plus-minus-box').data('id');
        if ($button.text() == "+") {
            quantity = parseInt(quantity) + 1;
        }
        else
        {
            // Don't allow decrementing below zero
            if (quantity > 1) {
                quantity = parseFloat(quantity) - 1;
            } else {
                quantity = 1;
            }
        }
        $button.parent().find("input").val(quantity);
        updateQuantity(id, quantity);
        updateCartPageDisplay(cartConfig.cartItemsTableId);
    });
}

function handleRemoveFromCartPage(id) {
    removeCartItem(id);
    updateCartPageDisplay(cartConfig.cartItemsTableId);
}

function updateCartPageDisplay(elementId) {
    const cartInfo = getCartInfo();
    const cartSubTotalAmount = $('#cartSubTotalAmount');
    const cartTotalAmount = $('#cartTotalAmount');
    const cartTable = $('#cartTable');
    const emptyCartInfo = $('#emptyCartInfo');
    const checkoutInfo = $('#checkoutInfo');
    const cartLogin = $('#cartLogin');

    if (cartInfo.totalItemsCount > 0) {
        cartTable.show();
        checkoutInfo.show();
        emptyCartInfo.hide();
    } else {
        cartTable.hide();
        checkoutInfo.hide();
        emptyCartInfo.show();
    }
    if (isAuthenticated()) {
        cartLogin.hide();
    } else {
        cartLogin.show();
    }
    cartSubTotalAmount.html(`₦${cartInfo.totalAmount}`);
    cartTotalAmount.html(`₦${cartInfo.totalAmount}`);
    bindCartItemsPage(elementId);
    updateCartInfoDisplay(cartConfig.cartItemsId);
}



