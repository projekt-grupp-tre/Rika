//function submitNewProduct(event) {
//    event.preventDefault();
//    const formData = new FormData(document.getElementById('addProductForm'));

//    const productData = {
//        name: formData.get('Name'),
//        description: formData.get('Description'),
//        categoryId: formData.get('CategoryId'),
//        images: formData.getAll('Images[]').map(url => ({ url })),
//        variants: [],
//        review: {
//            clientName: formData.get('ProductReview.ClientName'),
//            rating: parseInt(formData.get('ProductReview.Rating'), 10),
//            comment: formData.get('ProductReview.Comment')
//        }
//    };

//    const sizes = formData.getAll('Variants[]');
//    const colors = formData.getAll('VariantColors[]');
//    const stocks = formData.getAll('VariantStocks[]');
//    const prices = formData.getAll('VariantPrices[]');

//    sizes.forEach((size, index) => {
//        productData.variants.push({
//            size,
//            color: colors[index],
//            stock: parseInt(stocks[index], 10),
//            price: parseFloat(prices[index])
//        });
//    });

//    const graphqlQuery = {
//        query: `mutation AddClothingProduct($input: AddProductInput!) { 
//            addProduct(input: $input) { 
//                productId 
//                name 
//                description 
//                category { name } 
//                variants { size color stock price } 
//                reviews { clientName rating comment } 
//            } 
//        }`,
//        variables: { input: productData }
//    };

//    const loadingIndicator = document.getElementById('loadingIndicator');
//    loadingIndicator.style.display = 'block';

//    fetch('https://productprovidergraphql.azurewebsites.net/api/GraphQL?code=0GQhXGiLSYJRnfNBuRrB1_csNX6zQjBWwiUQgHPZb8pPAzFuI7EMSQ%3D%3D', {
//        method: 'POST',
//        headers: { 'Content-Type': 'application/json' },
//        body: JSON.stringify(graphqlQuery)
//    })
//        .then(response => response.json())
//        .then(data => {
//            alert('Product added successfully!');
//        })
//        .catch(() => {
//            alert('An error occurred. Please try again.');
//        })
//        .finally(() => {
//            loadingIndicator.style.display = 'none';
//        });
//}


function getCategoryName(categoryId) {
    const categories = { 1: "Clothes", 2: "Shoes", 3: "Bags", 4: "Electronics", 5: "Jewelry" };
    return categories[categoryId] || "Uncategorized";
}

function addVariantField() {
    const variantFieldsContainer = document.getElementById('variantFields');

    const sizeField = document.createElement('input');
    sizeField.type = 'text';
    sizeField.name = 'Variants[]';
    sizeField.placeholder = 'Variant Size';

    const colorField = document.createElement('input');
    colorField.type = 'text';
    colorField.name = 'VariantColors[]';
    colorField.placeholder = 'Variant Color';

    const stockField = document.createElement('input');
    stockField.type = 'number';
    stockField.name = 'VariantStocks[]';
    stockField.placeholder = 'Stock Quantity';

    const priceField = document.createElement('input');
    priceField.type = 'number';
    priceField.name = 'VariantPrices[]';
    priceField.placeholder = 'Price';

    variantFieldsContainer.appendChild(sizeField);
    variantFieldsContainer.appendChild(colorField);
    variantFieldsContainer.appendChild(stockField);
    variantFieldsContainer.appendChild(priceField);
}

function addImageField() {
    const imageFieldsContainer = document.getElementById('imageFields');
    const imageUrlField = document.createElement('input');
    imageUrlField.type = 'text';
    imageUrlField.name = 'Images[]';
    imageUrlField.placeholder = 'Image URL';
    imageFieldsContainer.appendChild(imageUrlField);
}

document.getElementById("categorySelect").addEventListener("change", function () {
    const categoryId = this.value;
    const categoryName = getCategoryName(categoryId);  // Få kategori namn
    document.getElementById("categoryName").value = categoryName;  // Sätt kategori namn i det osynliga fältet
});

function getCategoryName(categoryId) {
    const categories = {
        "1": "Clothes",
        "2": "Shoes",
        "3": "Bags",
        "4": "Electronics",
        "5": "Jewelry"
    };
    return categories[categoryId] || "Uncategorized";
}