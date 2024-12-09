document.addEventListener('DOMContentLoaded', () => {
    const form = document.getElementById('userProfileForm');
    const saveBtn = document.getElementById('saveBtn');
    const cancelBtn = document.getElementById('cancelBtn');
    const exitBtn = document.getElementById('exitBtn');
    
    let originalFormData = new FormData(form);
    let hasChanges = false;

    // Setup address autocomplete
    setupAddressAutocomplete('City');
    setupAddressAutocomplete('Street');

    // Form change detection
    form.addEventListener('input', () => {
        const currentFormData = new FormData(form);
        hasChanges = false;

        for (let [key, value] of currentFormData.entries()) {
            if (value !== originalFormData.get(key)) {
                hasChanges = true;
                break;
            }
        }

        toggleActionButtons(hasChanges);
    });

    // Button handlers
    saveBtn.addEventListener('click', async (e) => {
        e.preventDefault();
        if (form.checkValidity()) {
            try {
                const formData = new FormData(form);
                const response = await fetch(form.action, {
                    method: 'POST',
                    body: formData
                });

                if (response.ok) {
                    originalFormData = new FormData(form);
                    hasChanges = false;
                    toggleActionButtons(false);
                    showSuccessMessage('Changes saved successfully');
                } else {
                    showErrorMessage('Failed to save changes');
                }
            } catch (error) {
                showErrorMessage('An error occurred while saving');
            }
        } else {
            form.reportValidity();
        }
    });

    cancelBtn.addEventListener('click', () => {
        for (let [key, value] of originalFormData.entries()) {
            const input = form.elements[key];
            if (input) {
                input.value = value;
            }
        }
        hasChanges = false;
        toggleActionButtons(false);
    });

    exitBtn.addEventListener('click', () => {
        if (hasChanges) {
            if (confirm(document.getElementById('unsavedChangesMessage').value)) {
                window.location.href = '/';
            }
        } else {
            window.location.href = '/';
        }
    });
});

function toggleActionButtons(show) {
    const saveBtn = document.getElementById('saveBtn');
    const cancelBtn = document.getElementById('cancelBtn');
    saveBtn.classList.toggle('hidden', !show);
    cancelBtn.classList.toggle('hidden', !show);
}

async function setupAddressAutocomplete(fieldId) {
    const input = document.getElementById(fieldId);
    const suggestionsContainer = document.getElementById(fieldId.toLowerCase() + 'Suggestions');
    let debounceTimer;

    input.addEventListener('input', () => {
        clearTimeout(debounceTimer);
        debounceTimer = setTimeout(async () => {
            const value = input.value.trim();
            if (value.length < 2) {
                suggestionsContainer.style.display = 'none';
                return;
            }

            try {
                const response = await fetch(`/api/address/suggest?field=${fieldId}&query=${encodeURIComponent(value)}`);
                if (response.ok) {
                    const suggestions = await response.json();
                    displaySuggestions(suggestions, suggestionsContainer, input);
                }
            } catch (error) {
                console.error('Failed to fetch suggestions:', error);
            }
        }, 300);
    });

    // Hide suggestions when clicking outside
    document.addEventListener('click', (e) => {
        if (!input.contains(e.target) && !suggestionsContainer.contains(e.target)) {
            suggestionsContainer.style.display = 'none';
        }
    });
}

function displaySuggestions(suggestions, container, input) {
    if (!suggestions || suggestions.length === 0) {
        container.style.display = 'none';
        return;
    }

    container.innerHTML = '';
    suggestions.forEach(suggestion => {
        const div = document.createElement('div');
        div.className = 'suggestion-item';
        div.textContent = suggestion;
        div.addEventListener('click', () => {
            input.value = suggestion;
            container.style.display = 'none';
            input.dispatchEvent(new Event('input'));
        });
        container.appendChild(div);
    });
    container.style.display = 'block';
}

function showSuccessMessage(message) {
    // Implement your preferred notification system
    alert(message);
}

function showErrorMessage(message) {
    // Implement your preferred notification system
    alert(message);
}
