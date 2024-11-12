function addImageField() {
    const container = document.getElementById('imageFields');
    const div = document.createElement('div');
    div.classList.add('mb-2', 'd-flex', 'align-items-center');
    div.innerHTML = `
            <input type="text" class="form-control me-2" name="Images" placeholder ="ImageURL"/>
            <button type="button" class="btn" onclick="removeImageField(this)">Remove</button>
        `;
    container.appendChild(div);
}

function removeImageField(button) {
    button.parentElement.remove();
}