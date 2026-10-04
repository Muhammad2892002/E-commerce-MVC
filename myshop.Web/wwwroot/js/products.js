$(document).ready(function () {

    $("#mytable").DataTable({
        ajax: {
            url: "/Product/GetData",
            type: "GET",
            dataSrc: "data"
        },
        columns: [
            { data: "name" },
            { data: "description" },
            { data: "price" },
            { data: "categoryName" },
            {
                data: "id",
                render: function (id) {
                    return `
                        <a href="/Product/Edit/${id}" class="btn btn-success btn-sm">
                            <i class="fa-solid fa-pen"></i>
                        </a>

                        <button class="btn btn-danger btn-sm">
                            <i class="fa-solid fa-trash"></i>
                        </button>
                    `;
                }
            }
        ],
        autoWidth: false,
        scrollX: true
    });

});


function fillimg(event) {
    var imgholder = document.getElementById("ImagePreview");
    imgholder.src = URL.createObjectURL(event.target.files[0]);
}

document.addEventListener("DOMContentLoaded", function () {

    var btnSubmit = document.getElementById("btnSubmit");
    var inputImg = document.getElementById("imgInput");
    var imgPreview = document.getElementById("ImagePreview");
    var priceIN = document.getElementById("priceIn");

 
    priceIN.addEventListener("blur", function () {
        let val = parseFloat(priceIN.value);
        let msgSpan = document.getElementById("msg");

        if (val < 0 || val > 100000) {
            msgSpan.innerHTML = "Price must be larger than zero and less than million";
            btnSubmit.disabled = true;
        } else {
            msgSpan.innerHTML = "";
            btnSubmit.disabled = false;
        }
    });

  
    if (inputImg) {
        inputImg.addEventListener("change", function () {
            const maxSize = 2 * 1024 * 1024;
            const file = inputImg.files[0];

            if (file.size > maxSize) {
                alert("The image is bigger than 2MB");
                btnSubmit.disabled = true;
                inputImg.value = "";
                imgPreview.src = "";
            } else {
                btnSubmit.disabled = false;
            }
        });
    }

});




