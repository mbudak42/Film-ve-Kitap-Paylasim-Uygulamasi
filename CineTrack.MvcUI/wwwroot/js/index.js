let currentFilter = 'all';
let page = 1;

// Filtreleme fonksiyonu
function filterFeed(filter, event) {
    currentFilter = filter;

    // Aktif sekmeyi güncelle
    document.querySelectorAll('.filter-tab').forEach(tab => {
        tab.classList.remove('active');
    });
    event.target.classList.add('active');

    console.log('Filtering by:', filter);

    // API çağrısı (yorum satırı, backend hazır olunca aktif edilecek)
    /*
    fetch(`/api/feed?filter=${filter}&page=1`)
        .then(response => response.json())
        .then(data => updateFeed(data));
    */
}

// Beğeni (like) butonu işlemi
function toggleLike(button) {
    button.classList.toggle('liked');
    const likeSpan = button.querySelector('span');
    const currentCount = parseInt(likeSpan.textContent.match(/\d+/)[0]);

    if (button.classList.contains('liked')) {
        likeSpan.textContent = `${currentCount + 1} Beğeni`;
    } else {
        likeSpan.textContent = `${currentCount - 1} Beğeni`;
    }

    // API çağrısı (yorum satırı, backend hazır olunca aktif edilecek)
    /*
    fetch('/api/activity/like', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ activityId: activityId })
    });
    */
}

// Daha fazla içerik yükleme
function loadMore() {
    page++;
    console.log('Loading page:', page);

    // API çağrısı (yorum satırı)
    /*
    fetch(`/api/feed?filter=${currentFilter}&page=${page}`)
        .then(response => response.json())
        .then(data => appendToFeed(data));
    */
}

// Sonsuz kaydırma (isteğe bağlı alternatif)
window.addEventListener('scroll', () => {
    if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight - 500) {
        // loadMore(); // Eğer "Daha Fazla Yükle" butonu yerine otomatik kaydırma istersen burayı aktif et
    }
});
