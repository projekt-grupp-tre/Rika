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

