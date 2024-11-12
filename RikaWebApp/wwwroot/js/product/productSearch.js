document.addEventListener("DOMContentLoaded", function () {
    const searchInput = document.getElementById("searchInput");
    const productList = document.getElementById("productList");
    if (searchInput && productList) {
        const productRows = productList.getElementsByTagName("tr");

        searchInput.addEventListener("input", function () {
            const filter = searchInput.value.toLowerCase();

            for (let i = 0; i < productRows.length; i++) {
                const productId = productRows[i].getAttribute("data-id").toLowerCase();
                const productName = productRows[i].getAttribute("data-name").toLowerCase();
                const productCategory = productRows[i].getAttribute("data-category").toLowerCase();

                if (productId.includes(filter) || productName.includes(filter) || productCategory.includes(filter)) {
                    productRows[i].style.display = "";
                } else {
                    productRows[i].style.display = "none";
                }
            }
        });
    }
});