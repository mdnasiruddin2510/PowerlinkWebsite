
function addToCart(id, url, name, price, itemId, freeItemId, freeQty, originalUnitPrice) {
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
        }
        else {
            cart.push({ productId: id, productImageUrl: url, name: name, price: price, code: code, quantity: qty, discountPrice: discountPrice, priceTotal: parseFloat(price) });
            localStorage.setItem('cart', JSON.stringify(cart));
        }

    }
    else {
        cart.push({ productId: id, productImageUrl: url, name: name, price: price, code: code, quantity: qty, discountPrice: discountPrice, priceTotal: parseFloat(price) });
        localStorage.setItem('cart', JSON.stringify(cart));
    }
    $(".cartItemCount").text(cart.length);
    $(".addTadaClass").addClass("animated tada");
    setTimeout(function () {
        $(".addTadaClass").removeClass("animated tada");
    },1000);
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
    if (cart == null) {
        cart = [];
    }
    if (cart.length > 0) {
        let content = ``;
        $.each(cart, function (index, val) {
            content += `<div class="cart-table-prd">
                            <div class="cart-table-prd-image"><a href="javascript:void(0)"><img src="${imageURL + val.productImageUrl}" alt=""></a></div>
                            <div class="cart-table-prd-name">
                                <h4><a href="/product/details?id=${val.productId}">${val.code}</a></h4>
                                <h2><a href="/product/details?id=${val.productId}">${val.name}</a></h2>
                            </div>
                            <div class="cart-table-prd-qty"><span>qty:</span><b><a href="javascript:minusItem(${index})" title="Decrease Order Quantity" class="icon-prev"><i class="bx bx-chevron-left"></i></a><span class="quentityItemCart-${index}">${val.quantity}</span><a style="cursor:pointer;" href="javascript:plusItem(${index})" title="Increase Order Quantity" class="icon-next"><i class="bx bx-chevron-right"></i></a></b></div>
                            <div class="cart-table-prd-price"><span>price:</span> <b>৳ ${(val.priceTotal).toFixed(2)}</b></div>
                            <div class="cart-table-prd-action"><a href="javascript:removeItem(${val.productId})" title="Remove From Cart" class="icon-cross"><i class="bx bx-x"></i></a></div>
                        </div>`;
            priceTotal += val.priceTotal;
            itemTotal += 1;
        });
        const initialValue = 0;
        const total = cart.reduce((acc, curr) => acc + parseFloat(curr.priceTotal), initialValue);
        $("#cart-table-dy").empty();
        $("#cart-table-dy").append(content);
        $(".card-total-price").text('৳ ' + (priceTotal).toFixed(2));
        let deliveryCharge = 60;
        $("#deliveryCharge").text('৳ ' + (deliveryCharge).toFixed(2));
        $("#grandTotal").text('৳ ' + (priceTotal + deliveryCharge).toFixed(2));
        $(".cartItemCount").text(itemTotal);
    }
    else
    {
        let content = `<tr>
                            <th>Opps! Cart is empty. Please add item to your cart.</th>
                        </tr>`;
        $("#cart-table-dy").empty();
        $("#cart-table-dy").append(content);
        $(".cartItemCount").text(0);
        $(".card-total-price").text('৳ ' + (0).toFixed(2));
        $("#deliveryCharge").text('৳ ' + (0).toFixed(2));
        $("#grandTotal").text('৳ ' + (0).toFixed(2));
        $(".checkOutBtn").prop("disabled",true);
    }
}
let clearCart = () => {
    localStorage.removeItem('cart');
    cart = [];
    cartItems();
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
let plusItem = (objIndex) => {
    var cart = JSON.parse(localStorage.getItem('cart'));
    cart[objIndex].quantity += 1;
    let priceTotal = parseFloat(cart[objIndex].quantity * cart[objIndex].price)
    cart[objIndex].priceTotal = priceTotal;
    localStorage.setItem('cart', JSON.stringify(cart));
    cartItems();
}
let minusItem = (objIndex) => {
    var cart = JSON.parse(localStorage.getItem('cart'));
    var cartQty = parseInt(cart[objIndex].quantity) - 1;
    if (cartQty == 0) {
        removeItem(cart[objIndex].productId);
    } else {
        cart[objIndex].quantity -= 1;
        let priceTotal = parseFloat(cart[objIndex].quantity * cart[objIndex].price)
        cart[objIndex].priceTotal = priceTotal;
        localStorage.setItem('cart', JSON.stringify(cart));
    }
    cartItems();
}

let placeOrder = () => {
    var cart = JSON.parse(localStorage.getItem('cart'));
    var customer = JSON.parse(localStorage.getItem('customer'));

    let subTotal = cart.reduce((acc, curr) => acc + parseFloat(curr.priceTotal), 0);
    let couponPrice = 0;
    let deliveryCharge = 0;
    let order = {
        CustomerId: customer.id,
        GndTotal: (subTotal + deliveryCharge) - couponPrice, //confused
        PaidAmount: 0,
        SalesNote: "NA",
        DiscountAmount: couponPrice,
        DiscountPercentage: 0,
        SubTotal: subTotal,
        TotalOriginalAmount: subTotal + deliveryCharge,
        TotalProductDiscount: couponPrice, //confused
        OrderGroup: "NA",
        DeliveryAddress: "NA",
        DeliveryCharge: deliveryCharge,
        appId: appId
    };
    let orderItems = [];
    $.each(cart, function (index, val) {
        let obj = {

            ItemId: val.itemId,
            ProductId: JSON.stringify(val.productId),
            Qty: val.quantity,
            Amount: parseFloat(val.priceTotal),
            Price: parseFloat(val.price),
            Name: val.name,
            OriginalUnitPrice: val.originalUnitPrice,
            Discount: val.discountPrice,
            Comments: "Test by Bipu",
            FreeItemId: val.freeItemId,
            FreeQty: val.freeQty
        }
        orderItems.push(obj);
    });

    order.Products = orderItems;

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: baseURL + '/api/CustomerOrder/PlaceOrder',
        dataType: "json",
        type: 'POST',
        data: JSON.stringify(order),
        success: function (res) {
            setTimeout(function () {
                showAlert("Order has been placed Successfully..!", "success");
            }, 1000);
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
            customerId: customer.id
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

let getCountry = () => {
    let jsonObj = country_and_states.country;
    $("#country").empty();
    let content = `<option value="">Select Country</option>`;
    $.each(jsonObj, function (key, val) {
        content += `<option value="${key}">${val}</option>`;
    });
    $("#country").append(content);
}
$(document).on('change', '#country', function () {
    let selectedCountry = $("#country").val();
    if(selectedCountry == ""){
        $("#state").prop("disabled", true);
    }
    else{
        $("#state").prop("disabled", false);
    }
    let jsonObj = country_and_states.states[selectedCountry];
    $("#state").empty();
    let content = `<option value="">Select State</option>`;
    $.each(jsonObj, function (key, val) {
        content += `<option value="${val.code}">${val.name}</option>`;
    });
    $("#state").append(content);
});

/*Customer Part*/

let getCustomerInformation = () => {
    var customer = JSON.parse(localStorage.getItem('customer')); 
    if (!customer) {
        window.location.href = "/login/index";
    } else {
        let content = `<h3>Personal Info</h3>
                       <p>
                           <b>Name:</b> ${customer.name}<br />
                           <b>E-mail:</b> ${customer.email}<br />
                           <b>Phone:</b> ${customer.phone}<br />
                           <b>Address:</b> ${customer.address}<br />
                           <b>Country:</b> ${customer.countryName}<br />
                           <b>State:</b> ${customer.stateName}<br />
                           <b>City:</b> ${customer.city}<br />
                           <b>Postal Code:</b> ${customer.postalCode}
                       </p>
                       <div class="mt-2 clearfix">
                           <a href="#" class="link-icn js-show-form" data-form="#updateDetails" onclick="editCustomer()">
                               <i class="bx bx-pencil"></i>Edit
                           </a>
                       </div>`;
        $("#personalInfo").empty();
        $("#personalInfo").append(content);
    }
}

let editCustomer = () => {
    var customer = JSON.parse(localStorage.getItem('customer'));
    if (customer) {
        getCountry();
        $("#customerId").val(customer.id);
        $("#name").val(customer.name);
        $("#profileName").text(customer.email);
        $("#userName").val(customer.username == "" ? customer.email : customer.username);
        $("#email").val(customer.email);
        $("#phone").val(customer.phone);
        $("#address").val(customer.address);
        $("#country").val(customer.country);
        $("#state").val(customer.state);
        $("#city").val(customer.city);
        $("#postalCode").val(customer.zip);
    } else if (document.cookie != '') {
        let cookieName = decodeURIComponent(document.cookie.split(';')[0].split('=')[1]);
        let cookieEmail = decodeURIComponent(document.cookie.split(';')[1].split('=')[1]);
        $("#name").val(cookieName);
        $("#email").val(cookieEmail);
        $("#userName").val(cookieEmail);
        //$("#profileName").text(cookieEmail);
    }
    $("#updateDetails").show();
}

let updateCustomer = () => {
    var customer = JSON.parse(localStorage.getItem('customer'));
    if (!customer) {
        window.location.href = "/login/index";
    } else {
        let name = $("#name").val();
        let phone = $("#phone").val();
        let userName = $("#userName").val();
        let email = $("#email").val();
        let address = $("#address").val();
        let country = $("#country").val();
        let state = $("#state").val();
        let city = $("#city").val();
        let zip = $("#postalCode").val();
        var model = {
            id = customer.id,
            name,
            phone,
            address,
            userName,
            email,
            appKey: appId,
            country,
            state,
            city,
            zip
        }
        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: baseURL + '/api/Customer/UpdateCustomer',
            dataType: "json",
            type: "post",
            data: JSON.stringify(model),
            success: function (res) {
                if (res != "") {
                    getCustomerInformation()
                    .then(() => {
                            showAlert(res);
                            setTimeout(function () {
                                window.location.href = "/checkout/index";
                            }, 1000);
                    });
                        
                }
                else {
                    showAlert("Customer Not Found!!");
                }
            },
            error: function (a, b, c) {
                //debugger;
            }
        });
    }
}

let wishListProducts = () => {
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
                $.each(res, function (index, val) {
                    content += `<div class="cart-table-prd">
                                    <div class="cart-table-prd-image"><a href="javascript:void(0)"><img src="${imageURL + val.productImageUrl}" alt=""></a></div>
                                    <div class="cart-table-prd-name">
                                        <h4><a href="/product/details?id=${val.id}">${val.code}</a></h4>
                                        <h2><a href="/product/details?id=${val.id}">${val.name}</a></h2>
                                    </div>
                                    <div class="cart-table-prd-price"><span id="price">price:</span> <b>${currencySymbol + val.price}</b></div>
                                    <div class="cart-table-prd-action" style="width:2px;"><a href="javascript:removeItem(${val.id})" title="Remove From Wishlist" class="icon-cross"><i class="bx bx-x"></i></a></div>
                                    <div class="cart-table-addtocart">
                                        <a href="addToCart(${val.id},'${val.productImageUrl}','${val.name.replace(/[^a-zA-Z ]/g, "")}','${val.price}',${val.itemId},${val.freeItemId},'${val.freeQty}','${val.originalUnitPrice}')" class="btn btnAddToCart">Add To Cart</a>
                                    </div>
                                </div>`;
                });
                $("#wishList").empty();
                $("#wishList").append(content);
            }
        });
    }
    else{
        //showAlert("Please Login or Sign Up..!!", "error");
        window.location.href = "/login/index";
    }
}
let removeWishlist = (id) => {
    var customer = JSON.parse(localStorage.getItem('customer'));
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: baseURL + '/api/Customer/RemoveWishList',
        dataType: "json",
        type: 'GET',
        data: { customerId: customer.id, productId: +id },
        success: function (res) {
            if (res == 1) {
                showAlert("Removed from WishList Successfully..!!", "success");
            } else if(res == 0) {
                showAlert("Removed from WishList Failed..!!", "error");
            }
            wishListProducts();
        }
    });
}
let orderHistory = () => {
    var customer = JSON.parse(localStorage.getItem('customer'));
    if (customer){
        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: baseURL + '/api/Customer/GetOrderHistory',
            dataType: "json",
            type: 'GET',
            data: { customerId: customer.id },
            success: function (res) {
                let content = ``;
                $.each(res, function (index, val) {
                    content += `<tr>
                                    <td>1</td>
                                    <td><b>PM-${val.code}</b><a href="/product/details?id=${val.id}" class="ml-1" style="margin-left:7px;margin-right:7px;">View Details</a><a href="/product/details?id=${val.id}" class="ml-1">Track</a></td>
                                    <td>${val.orderDate}</td>
                                    <td>${val.status}</td>
                                    <td><span class="color">${currencySymbol + val.price}</span></td>
                                    <td style="text-align:right;"><a style="background-color:transparent !important;" href="#" class="btn" onclick="placeReOrder(${val.id})">REORDER</a></td>
                                </tr>`;
                });
                $("#orderHistory tbody").empty();
                $("#orderHistory tbody").append(content);
            }
        });
    }
    else{
        window.location.href = "/home/index";
    }
}