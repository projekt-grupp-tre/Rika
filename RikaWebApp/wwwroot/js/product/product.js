document.querySelectorAll('.favorite-btn').forEach(button => {
    button.addEventListener('click', function (e) {
        e.preventDefault();

        const icon = this.querySelector('i');

        if (icon.classList.contains('fa-regular')) {
            icon.classList.remove('fa-regular');
            icon.classList.add('fa-solid');
        } else {
            icon.classList.remove('fa-solid');
            icon.classList.add('fa-regular');
        }
    });
});

const searchToggle = document.getElementById('search-toggle');
const searchBar = document.getElementById('search-bar');

searchToggle.addEventListener('click', function () {
    if (searchBar.style.display === 'none' || searchBar.style.display === '') {
        searchBar.style.display = 'block';
        searchBar.querySelector('input').focus();
    } else {
        searchBar.style.display = 'none';
    }
});

document.addEventListener("DOMContentLoaded", function () {
    var searchBars = document.querySelectorAll("#search-bar");

    searchBars.forEach(function (searchBar) {
        searchBar.addEventListener("input", function () {
            var searchTerm = this.value.toLowerCase();
            var parentSection = this.closest(".container, .reviews-container, .category-selection");

            if (parentSection) {
                var items = parentSection.querySelectorAll(".grid a, .reviews-list .review-card, .product-category .grid a");

                items.forEach(function (item) {
                    var itemText = item.innerText.toLowerCase();

                    if (itemText.includes(searchTerm)) {
                        item.style.display = "";
                    } else {
                        item.style.display = "none";
                    }
                });
            }
        });
    });
});


