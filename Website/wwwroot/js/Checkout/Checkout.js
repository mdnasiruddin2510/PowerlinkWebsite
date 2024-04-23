﻿
function addToCart(id, url, name, price, discountPrice) {
    var cart = [];
    var cart = JSON.parse(localStorage.getItem('cart'));
    if (cart == null) {
        cart = [];
    }
    let qty = +$(".cart-quantity-input").val();
    if (isNaN(qty)) {
        qty = 1;
    }
    let checkCart = cart.find(x => x.productId == id);
    if (checkCart != null) {
        var objIndex = cart.findIndex((obj => obj.productId == id));
        if (objIndex != -1) {
            cart[objIndex].quantity += qty;
            let priceTotal = parseFloat(cart[objIndex].quantity * cart[objIndex].price)
            cart[objIndex].priceTotal = priceTotal;
            localStorage.setItem('cart', JSON.stringify(cart));
            //showAlert("Cart Updated Successfully..!", "success");
        }
        else {
            cart.push({ productId: id, productImageUrl: url, name: name, price: price, quantity: qty, discountPrice: discountPrice, priceTotal: parseFloat(price) });
            localStorage.setItem('cart', JSON.stringify(cart));
            //showAlert("Added To Cart Successfully..!", "success");
        }

    }
    else {
        cart.push({ productId: id, productImageUrl: url, name: name, price: price, quantity: qty, discountPrice: discountPrice, priceTotal: parseFloat(price) });
        localStorage.setItem('cart', JSON.stringify(cart));
        //showAlert("Added To Cart Successfully..!", "success");
    }
    $(".cartItemCount").text(cart.length);
    $(".addTadaClass").addClass("animated tada");
    setTimeout(function () {
        $(".addTadaClass").removeClass("animated tada");
    }, 3000);
}
let showAlert = (message, icon) => {
    Swal.fire({
      position: "center",
      icon: icon,
      title: message,
      showConfirmButton: false,
      timer: 1000
    });
}
function cartItems() {
    var priceTotal = 0;
    var itemTotal = 0;
    var cart = JSON.parse(localStorage.getItem('cart'));

    if (cart.length > 0) {
        let content = ``;
        $.each(cart, function (index, val) {
            content += `<tr>
                            <td><img class="rounded" src="${imageURL + val.productImageUrl}" alt=""></td>
                            <td><a class="product-title" href="/shop/details?id=${val.productId}">${val.name}<span class="mt-1">${currencySymbol + val.price}</span></a></td>
                            <td>
                                <div class="quantity">
                                    <input class="qty-text decimal" type="text" min="1" id="quantityChanged${index}" max="99" onblur="modifyQuantity(${index})" value="${val.quantity}">
                                </div>
                            </td>
                            <th scope="row"><a class="remove-product" onclick="removeItem(${val.productId})" href=""><i class="ti ti-x"></i></a></th>
                        </tr>`;
            priceTotal += parseInt(val.priceTotal);
            itemTotal += 1;
        });
        const initialValue = 0;
        const total = cart.reduce((acc, curr) => acc + parseFloat(curr.priceTotal), initialValue);
        $("#cartTbl tbody").empty();
        $("#cartTbl tbody").append(content);
        $(".priceTotal").text(total.toFixed(2));
        $(".cartItemCount").text(itemTotal);
    }
    else
    {
        let content = `<tr>
                            <th>Opps! Cart is empty. Please add item to your cart.</th>
                        </tr>`;
        $("#cartTbl tbody").empty();
        $("#cartTbl tbody").append(content);
        $("#checkOutBtn").prop("disabled",true);
    }
}
let removeItem = (productId) => {
    var cart = JSON.parse(localStorage.getItem('cart'));
    if (cart.length > 0) {
        localStorage.setItem('cart', JSON.stringify(removeObjectWithId(cart, productId)));
        cartItems();
    }
}
function removeObjectWithId(arr, id) {
    const objWithIdIndex = arr.findIndex((obj) => obj.productId === id);
    arr.splice(objWithIdIndex, 1);
    return arr;
}
let modifyQuantity = (objIndex) => {
    var cart = JSON.parse(localStorage.getItem('cart'));
    cart[objIndex].quantity = +$("#quantityChanged" + objIndex).val();
    let priceTotal = parseFloat(cart[objIndex].quantity * cart[objIndex].price)
    cart[objIndex].priceTotal = priceTotal;
    localStorage.setItem('cart', JSON.stringify(cart));
    cartItems();
}
let placeOrder = () => {
    let productId = [];
    var cart = JSON.parse(localStorage.getItem('cart'));
    var customer = JSON.parse(localStorage.getItem('customer'));
    let requestBody = [];
    $.each(cart, function (index, val) {
        let obj = {
            ProductId: JSON.stringify(val.productId),
            CustomerId: customer.id,
            Price: parseFloat(val.price),
            Quantity: val.quantity,
            DiscountPrice: 0, //val.discountPrice,
            AdvanceAmount: 0,
            PriceTotal: val.priceTotal
        }
        requestBody.push(obj);
    });
    $.ajax({
        url: baseURL + '/api/Order/PlaceOrder',
        dataType: "json",
        contentType: 'application/json',
        type: 'POST',
        data: JSON.stringify(requestBody),
        success: function (res) {
            //window.location.href = "/checkout/paymentsuccess";
            showAlert("Order has been placed Successfully..!", "success");
            localStorage.removeItem('cart');
            window.location.href = "/checkout/cash";
        }
    });
}
$(document).on('click','.wishlist-btn', function(e) {
    e.preventDefault();
            
    var customer = JSON.parse(localStorage.getItem('customer'));
    if (customer){
        let hasClass = $('.ti ti-heart').hasClass('active');
        if (!hasClass) {
            $('.ti ti-heart').addClass('active');
        } else {
            $('.ti ti-heart').removeClass('active');
        } 
        let productId = +$(this).data("id");

        let data = {
            productId: productId,
            customerId: customer.id,
            phone: "01703504061",
            Email: "dhoor@gmail.com"
        }
        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: baseURL + '/api/Customer/WishList',
            dataType: "json",
            type: 'POST',
            data: JSON.stringify(data),
            success: function (res) {
                if (res == 1) {
                    showAlert("Added to wishlist Successfully !!", "success");
                } else {
                    showAlert("Remove from wishlist Successfully !!", "success");
                }
            }
        });
    }
    else{
        showAlert("Please Login or Sign Up..!!", "error");
    }
});

$(".preventBack").click(function () {
    window.history.back();
});
let preventBack = () => {
    window.history.back();
}

let wishListProducts = (showProductStyle) => {
    var customer = JSON.parse(localStorage.getItem('customer'));
    if (customer){
        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: baseURL + '/api/Customer/GetWishListByCustomerId',
            dataType: "json",
            type: 'GET',
            data: { customerId: customer.id },
            success: function (res) {
                let content = ``;
                if (showProductStyle == "grid") {
                    $.each(res, function (index, val) {
                         content += `<div class="col-6 col-md-4">
                                        <div class="card product-card">
                                            <div class="card-body">
                                                <span class="badge rounded-pill badge-warning">Sale</span>
                                                <a class="delete-btn" data-currentpage="grid" data-id="${val.id}" href="#"><i class="ti ti-trash"></i></a>
                                                <a class="product-thumbnail d-block" href="#">
                                                    <img class="mb-2" src="${imageURL + val.productImageUrl}" alt="">
                                                </a>
                                                <a class="product-title" href="#">${val.name}</a>
                                                <p class="sale-price">${currencySymbol + val.price}</p>
                                                <a class="btn btn-success btn-sm" href="javascript:addToCart(${val.id},'${val.productImageUrl}','${val.name}','${val.price}')"><i class="ti ti-plus"></i></a>
                                            </div>
                                        </div>
                                     </div>`;
                     });
                } else if (showProductStyle == "list") {
                    $.each(res, function (index, val) {
                        content += `<div class="col-12">
                                        <div class="card horizontal-product-card">
                                            <div class="d-flex align-items-center">
                                                <div class="product-thumbnail-side">
                                                    <a class="product-thumbnail d-block" href="#"><img src="${imageURL + val.productImageUrl}" alt=""></a>
                                                    <a class="delete-btn" data-currentpage="list" data-id="${val.id}" href="#"><i class="ti ti-trash"></i></a>
                                                </div>
                                                <div class="product-description">
                                                    <a class="product-title d-block" href="#">${val.name}</a>
                                                    <p class="sale-price">${currencySymbol + val.price}</p>
                                                </div>
                                            </div>
                                        </div>
                                     </div>`;
                     });
                }
                content += `<div class="col-12">
                                <div class="select-all-products-btn mt-2"><a class="btn btn-success w-100" href="#"><i class="ti ti-shopping-cart-cog me-2"></i>Add all items to cart</a></div>
                            </div>`;
                $("#productCard").empty();
                $("#productCard").append(content);
            }
        });
    }
    else{
        //showAlert("Please Login or Sign Up..!!", "error");
        window.location.href = "/login/index";
    }
}

$(document).on('click','.delete-btn', function(e) {
    e.preventDefault();
    let productId = +$(this).data('id');
    let page = $(this).data('currentpage');
    var customer = JSON.parse(localStorage.getItem('customer'));
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: baseURL + '/api/Customer/RemoveWishList',
        dataType: "json",
        type: 'GET',
        data: { customerId: customer.id, productId: productId },
        success: function (res) {
            if (res == 1) {
                showAlert("Removed from WishList Successfully..!!", "success");
            } else if(res == 0) {
                showAlert("Removed from WishList Failed..!!", "error");
            }
            wishListProducts(page);
        }
    });        
    
});