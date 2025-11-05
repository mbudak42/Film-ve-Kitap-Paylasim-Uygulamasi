let userRating = 0;

// Yıldız puanlama sistemi
const stars = document.querySelectorAll('.star');
const selectedRatingText = document.getElementById('selectedRating');

stars.forEach(star => {
    star.addEventListener('click', function() {
        userRating = parseInt(this.dataset.rating);
        updateStars();
        selectedRatingText.textContent = `Seçilen Puan: ${userRating}/10`;

        console.log('Rating selected:', userRating);

        // API çağrısı (yorum satırı)
        /*
        fetch('/api/content/rate', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ contentId: 1, rating: userRating })
        });
        */
    });

    star.addEventListener('mouseenter', function() {
        const rating = parseInt(this.dataset.rating);
        highlightStars(rating);
    });
});

document.getElementById('userRating').addEventListener('mouseleave', () => updateStars());

function highlightStars(rating) {
    stars.forEach((star, index) => {
        star.style.color = index < rating ? '#FFB300' : '#ddd';
    });
}

function updateStars() {
    stars.forEach((star, index) => {
        star.classList.toggle('active', index < userRating);
    });
}

function toggleList(listType, event) {
    const button = event.target;

    const active = button.classList.contains('btn-success');
    button.classList.toggle('btn-success', !active);
    button.classList.toggle('btn-secondary', active);
    button.innerHTML = active
        ? (listType === 'watched' ? '➕ İzledim' : '➕ İzlenecekler')
        : '✓ ' + (listType === 'watched' ? 'İzledim' : 'İzlenecekler');

    console.log(`List toggled: ${listType}, Active: ${!active}`);
}

function addToCustomList() {
    alert('Özel listeler burada görünecek (demo)');
}

function submitComment() {
    const text = document.getElementById('commentText').value.trim();

    if (!text) {
        alert('Lütfen bir yorum yazın.');
        return;
    }
    if (userRating === 0) {
        alert('Lütfen önce bir puan verin.');
        return;
    }

    console.log('Comment submitted:', { rating: userRating, text });

    alert('Yorumunuz eklendi! (Demo)');
    document.getElementById('commentText').value = '';
}

function toggleCommentLike(button) {
    button.classList.toggle('liked');
    const likeCount = button.querySelector('span');
    const count = parseInt(likeCount.textContent);

    likeCount.textContent = button.classList.contains('liked') ? count + 1 : count - 1;

    console.log('Comment like toggled');
}
