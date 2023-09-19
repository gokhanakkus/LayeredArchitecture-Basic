
// Modal'da seçilen cartId'yi al ve ilgili inputa ata
$('#deleteConfirmationModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var cartId = button.data('cartid');
    var modal = $(this);
    modal.find('#deleteCartId').val(cartId);
});


function incrementQuantity(inputId) {
    var input = document.getElementById(inputId);
    var currentValue = parseInt(input.value);
    input.value = currentValue + 1;
    updateTotalPrice(inputId);
}

function decrementQuantity(inputId) {
    var input = document.getElementById(inputId);
    var currentValue = parseInt(input.value);
    if (currentValue > 1) {
        input.value = currentValue - 1;
        updateTotalPrice(inputId);
    }
}
function updateTotalPrice(inputId) {
    var input = document.getElementById(inputId);
    var cartId = input.getAttribute('data-cartid');
    var quantity = parseInt(input.value);

    var totalPriceCell = document.querySelector(`.total-price[data-cartid="${cartId}"]`);
    var productPrice = parseFloat(totalPriceCell.getAttribute('data-price'));
    var total = productPrice * quantity;

    totalPriceCell.innerText = total.toFixed(2);

    var totalPrices = document.querySelectorAll('.total-price[data-cartid]');
    var grandTotal = totalPrices.reduce((acc, priceCell) => {
        var cartPrice = parseFloat(priceCell.innerText);
        return acc + cartPrice;
    }, 0);

    var grandTotalElement = document.getElementById('grandTotal');
    grandTotalElement.innerText = grandTotal.toFixed(2);
}

function confirmRemoveItem(productName, cartItemId) {
    var modal = $('#removeItemModal');
    modal.find('.modal-body p').text(`Are you sure you want to remove ${productName} from the cart?`);

    var confirmButton = modal.find('#confirmRemoveButton');
    confirmButton.off('click').on('click', function () {

        modal.modal('hide');

        $('form input[name="cartItemId"]').val(cartItemId);
        $('form').submit();
    });

    modal.modal('show');
}

function showNotification(button) {
    var notification = button.closest('.card-body').querySelector('.notification');
    notification.style.display = 'block';
    setTimeout(function () {
        notification.style.display = 'none';
    }, 4000);
}
document.querySelector('.theme-switch').addEventListener('click', () => {
    var isDarkModeEnabled = document.body.classList.toggle('dark-mode');
    document.cookie = `DarkMode=${isDarkModeEnabled};path=/;max-age=31536000`; // 1 yıl süreyle cookie'ye kaydet
});

window.addEventListener('load', () => {
    var isDarkModeEnabled = document.cookie.replace(/(?:(?:^|.*;\s*)DarkMode\s*\=\s*([^;]*).*$)|^.*$/, "$1") === 'true';
    if (isDarkModeEnabled) {
        document.body.classList.add('dark-mode');
    }
});