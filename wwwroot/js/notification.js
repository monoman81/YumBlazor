window.showToastr = (type, message) => {
    if (type == "success")
        toastr.success(message);
    if (type == "warning")
        toastr.warning(message);
    if (type == "error")
        toastr.error(message);
}

window.showSweetAlert = function (type, message) {
    if (type == "success") {
        Swal.fire({
            title: 'Good job!',
            text: message,
            icon: 'success'
        });
    }
    if (type == "error") {
        Swal.fire({
            title: 'Task failed!',
            text: message,
            icon: 'error'
        });
    }
    if (type == "warning") {
        Swal.fire({
            title: 'Proceed carefully!',
            text: message,
            icon: 'warning'
        });
    }
}