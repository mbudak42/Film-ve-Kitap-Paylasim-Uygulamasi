let currentContentType = 'all';
let searchActive = false;

function setContentType(type, event) {
    currentContentType = type;
    
    document.querySelectorAll('.type-tab').forEach(tab => {
        tab.classList.remove('active');
    });
    event.target.classList.add('active');

    if (searchActive) {
        performSearch(event);
    }
}

function performSearch(event) {
    const query = document.getElementById('searchInput').value.trim();
    
    if (query.length === 0) {
        hideSearchResults();
        return;
    }

    searchActive = true;
    showSearchResults();

    console.log('Searching for:', query, 'Type:', currentContentType);

    // API çağrısı (yorum satırı, backend hazır olunca aktif edilecek)
    /*
    fetch(`/api/search?q=${query}&type=${currentContentType}`)
        .then(response => response.json())
        .then(data => displaySearchResults(data));
    */

    // Demo mod
    document.getElementById('resultsCount').textContent = 42;
}

function showSearchResults() {
    document.getElementById('searchResults').style.display = 'block';
    document.getElementById('topRatedSection').style.display = 'none';
    document.getElementById('popularSection').style.display = 'none';
}

function hideSearchResults() {
    searchActive = false;
    document.getElementById('searchResults').style.display = 'none';
    document.getElementById('topRatedSection').style.display = 'block';
    document.getElementById('popularSection').style.display = 'block';
}

function applyFilters() {
    const genre = document.getElementById('genreFilter').value;
    const year = document.getElementById('yearFilter').value;
    const rating = document.getElementById('ratingFilter').value;
    const sort = document.getElementById('sortFilter').value;

    console.log('Filters:', { genre, year, rating, sort });

    /*
    fetch(`/api/discover?genre=${genre}&year=${year}&rating=${rating}&sort=${sort}&type=${currentContentType}`)
        .then(response => response.json())
        .then(data => updateContent(data));
    */
}

function clearFilters() {
    document.getElementById('genreFilter').value = '';
    document.getElementById('yearFilter').value = '';
    document.getElementById('ratingFilter').value = '';
    document.getElementById('sortFilter').value = 'popular';
    applyFilters();
}

function goToContent(type, id) {
    window.location.href = `/eser/${type}/${id}`;
}

// Enter tuşuyla arama
document.getElementById('searchInput').addEventListener('keypress', function(e) {
    if (e.key === 'Enter') {
        performSearch(e);
    }
});
