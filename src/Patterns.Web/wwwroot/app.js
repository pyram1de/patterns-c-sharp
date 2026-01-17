const API_URL = 'http://localhost:5000/api/inventory';

// Tab navigation
document.querySelectorAll('.tab-button').forEach(button => {
    button.addEventListener('click', function() {
        const tabName = this.getAttribute('data-tab');
        showTab(tabName);
    });
});

// Add item form submission
document.getElementById('add-item-form').addEventListener('submit', async function(e) {
    e.preventDefault();
    
    const itemId = document.getElementById('itemId').value;
    const itemName = document.getElementById('itemName').value;
    const quantity = parseInt(document.getElementById('quantity').value);
    
    try {
        const response = await fetch(`${API_URL}/items`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ itemId, itemName, quantity })
        });

        const data = await response.json();
        const messageDiv = document.getElementById('add-message');
        
        if (response.ok) {
            messageDiv.className = 'message success';
            messageDiv.textContent = '✓ Item added successfully!';
            document.getElementById('add-item-form').reset();
            // Auto-load items tab
            setTimeout(() => showTab('items'), 1500);
        } else {
            messageDiv.className = 'message error';
            messageDiv.textContent = `✗ Error: ${data.message}`;
        }
    } catch (error) {
        const messageDiv = document.getElementById('add-message');
        messageDiv.className = 'message error';
        messageDiv.textContent = `✗ Error: ${error.message}`;
    }
});

async function loadItems() {
    try {
        const response = await fetch(`${API_URL}/items`);
        const data = await response.json();
        
        const itemsList = document.getElementById('items-list');
        itemsList.innerHTML = '';
        
        if (data.items && data.items.length > 0) {
            data.items.forEach(item => {
                const itemCard = document.createElement('div');
                itemCard.className = 'item-card';
                itemCard.innerHTML = `
                    <div class="item-header">
                        <h3>${item.itemName}</h3>
                        <span class="item-id">${item.itemId}</span>
                    </div>
                    <div class="item-body">
                        <p><strong>Quantity:</strong> ${item.quantity}</p>
                    </div>
                    <div class="item-footer">
                        <button onclick="editQuantity('${item.itemId}')" class="btn btn-small">Edit</button>
                        <button onclick="deleteItem('${item.itemId}')" class="btn btn-small btn-danger">Delete</button>
                    </div>
                `;
                itemsList.appendChild(itemCard);
            });
        } else {
            itemsList.innerHTML = '<p class="empty-state">No items found. Add one to get started!</p>';
        }
    } catch (error) {
        console.error('Error loading items:', error);
        document.getElementById('items-list').innerHTML = `<p class="error">Error loading items: ${error.message}</p>`;
    }
}

async function deleteItem(itemId) {
    if (!confirm(`Are you sure you want to delete item ${itemId}?`)) return;
    
    try {
        const response = await fetch(`${API_URL}/items/${itemId}`, {
            method: 'DELETE'
        });
        
        if (response.ok) {
            alert('Item deleted successfully!');
            loadItems();
        } else {
            alert('Error deleting item');
        }
    } catch (error) {
        alert(`Error: ${error.message}`);
    }
}

async function editQuantity(itemId) {
    const newQuantity = prompt(`Enter new quantity for ${itemId}:`);
    if (newQuantity === null) return;
    
    if (isNaN(newQuantity) || newQuantity < 0) {
        alert('Please enter a valid quantity');
        return;
    }
    
    try {
        const response = await fetch(`${API_URL}/items/${itemId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ quantity: parseInt(newQuantity) })
        });
        
        if (response.ok) {
            loadItems();
        } else {
            alert('Error updating item');
        }
    } catch (error) {
        alert(`Error: ${error.message}`);
    }
}

async function loadHelp() {
    try {
        const response = await fetch(`${API_URL}/help`);
        const data = await response.json();
        
        const helpContent = document.getElementById('help-content');
        let html = '<div class="api-docs">';
        
        if (data.commands) {
            html += '<h3>Available Endpoints:</h3><ul>';
            data.commands.forEach(cmd => {
                html += `<li><strong>${cmd.command}:</strong> ${cmd.description}</li>`;
            });
            html += '</ul>';
        }
        
        html += `
            <h3>API Endpoints:</h3>
            <ul>
                <li><strong>GET /api/inventory/items</strong> - Get all items</li>
                <li><strong>GET /api/inventory/items/{id}</strong> - Get specific item</li>
                <li><strong>POST /api/inventory/items</strong> - Add new item</li>
                <li><strong>PUT /api/inventory/items/{id}</strong> - Update item quantity</li>
                <li><strong>DELETE /api/inventory/items/{id}</strong> - Delete item</li>
                <li><strong>GET /api/inventory/help</strong> - Get help</li>
            </ul>
        `;
        
        html += '</div>';
        helpContent.innerHTML = html;
    } catch (error) {
        document.getElementById('help-content').innerHTML = `<p class="error">Error loading help: ${error.message}</p>`;
    }
}

function showTab(tabName) {
    // Hide all tabs
    document.querySelectorAll('.tab-content').forEach(tab => {
        tab.classList.remove('active');
    });
    
    // Remove active from all buttons
    document.querySelectorAll('.tab-button').forEach(btn => {
        btn.classList.remove('active');
    });
    
    // Show selected tab
    document.getElementById(tabName).classList.add('active');
    event.target.classList.add('active');
    
    // Load content for specific tabs
    if (tabName === 'items') {
        loadItems();
    } else if (tabName === 'help') {
        loadHelp();
    }
}

// Load items on page load
window.addEventListener('load', () => {
    loadItems();
});
