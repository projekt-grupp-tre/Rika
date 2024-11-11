let variantIndex = document.querySelectorAll('[name^="Variants["]').length / 5 || 0;
let reviewIndex = document.querySelectorAll('[name^="Reviews["]').length / 3 || 0;

function addImageField() {
    const container = document.getElementById('imageFields');
    const div = document.createElement('div');
    div.classList.add('mb-2', 'd-flex', 'align-items-center');
    div.innerHTML = `
            <input type="text" class="form-control me-2" name="Images" />
            <button type="button" class="btn btn-danger" onclick="removeImageField(this)">Remove</button>
        `;
    container.appendChild(div);
}

function removeImageField(button) {
    button.parentElement.remove();
}

function addVariantField() {
    const container = document.getElementById('variantFields');
    const div = document.createElement('div');
    div.classList.add('variant-item', 'mb-3');
    div.innerHTML = `
            <input type="hidden" name="Variants[${variantIndex}].ProductVariantId"/>
            <label>Size</label>
                <input type="text" class="form-control mb-2" name="Variants[${variantIndex}].Size" required/>
            <label>Color</label>
                <input type="text" class="form-control mb-2" name="Variants[${variantIndex}].Color" required/>
            <label>Stock</label>
                <input type="number" class="form-control mb-2" name="Variants[${variantIndex}].Stock" required/>
            <label>Price</label>
                <input type="number" class="form-control mb-2" name="Variants[${variantIndex}].Price" step="0.01" required/>
            <button type="button" class="btn btn-danger mt-2" onclick="removeVariantField(this)">Remove Variant</button>
        `;
    container.appendChild(div);
    variantIndex++;
}

function removeVariantField(button) {
    button.parentElement.remove();
    variantIndex--;
}

function addReviewField() {
    const container = document.getElementById('reviewFields');
    const div = document.createElement('div');
    div.classList.add('review-item', 'mb-3');
    div.innerHTML = `
            <input type="hidden" name="Reviews[${reviewIndex}].ReviewId" />
            <label>Client Name</label>
                <input type="text" class="form-control mb-2" name="Reviews[${reviewIndex}].ClientName"required />
            <label>Rating</label>
                <input type="number" class="form-control mb-2" name="Reviews[${reviewIndex}].Rating" min="1" max="5" required/>
            <label>Comment</label>
                <textarea class="form-control mb-2" name="Reviews[${reviewIndex}].Comment" rows="2" required></textarea>
            <button type="button" class="btn btn-danger mt-2" onclick="removeReviewField(this)">Remove Review</button>
        `;
    container.appendChild(div);
    reviewIndex++;
}

function removeReviewField(button) {
    button.parentElement.remove();
    reviewIndex--;
}