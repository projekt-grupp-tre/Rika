function addImageField() {
    const imageFields = document.getElementById('imageFields');
    const imageDiv = document.createElement('div');
    imageDiv.classList.add('image-field');
    imageDiv.innerHTML = `
      <input type="text" name="imageUrl[]" placeholder="Image URL" required>
      <button class="trashcan" type="button" onclick="removeField(this)"><i class="fa-regular fa-trash-can"></i></button>
    `;
    imageFields.appendChild(imageDiv);
}

function addVariantField() {
    const variantFields = document.getElementById('variantFields');
    const variantDiv = document.createElement('div');
    variantDiv.classList.add('variant-field');
    variantDiv.innerHTML = `
      <h5>New variant</h5>
      <input type="text" name="variantSize[]" placeholder="Size" required>
      <input type="text" name="variantColor[]" placeholder="Color" required>
      <input type="text" name="variantStock[]" placeholder="Stock" required>
      <input type="text" name="variantPrice[]" placeholder="Price" required>
      <button type="button" onclick="removeField(this)"><i class="fa-sharp fa-solid fa-xmark"></i></button>
    `;
    variantFields.appendChild(variantDiv);
}

function removeField(button) {
    button.parentElement.remove();
}

function submitNewProduct(event) {
    event.preventDefault();
    const formData = new FormData(document.getElementById('addProductForm'));
    const productData = {
        name: formData.get('name'),
        description: formData.get('description'),
        categoryId: parseInt(formData.get('categoryId')),
        images: [],
        variants: []
    };

    // Get images
    const imageUrls = formData.getAll('imageUrl[]');
    productData.images = imageUrls;

    // Get variants
    const sizes = formData.getAll('variantSize[]');
    const colors = formData.getAll('variantColor[]');
    const stocks = formData.getAll('variantStock[]');
    const prices = formData.getAll('variantPrice[]');
    sizes.forEach((size, index) => {
        productData.variants.push({
            size,
            color: colors[index],
            stock: parseInt(stocks[index]),
            price: parseInt(prices[index])
        });
    });

    // Here you would send the productData to the server (e.g., using fetch or AJAX)
    console.log('Product Data:', productData);
    alert('Product added successfully!');
}