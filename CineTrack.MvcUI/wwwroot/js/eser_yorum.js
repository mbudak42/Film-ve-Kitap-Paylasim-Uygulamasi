let userRating = 0;

// Yıldız puanlama sistemi
const starsInput = document.querySelectorAll('.star-input');
const selectedRatingText = document.getElementById('selectedRatingText');

starsInput.forEach(star => {
    star.addEventListener('click', function() {
        userRating = parseInt(this.dataset.rating);
        updateStarsInput();
        selectedRatingText.textContent = `Seçilen Puan: ${userRating}/10`;
    });

    star.addEventListener('mouseenter', function() {
        highlightStarsInput(parseInt(this.dataset.rating));
    });
});

document.getElementById('starRatingInput').addEventListener('mouseleave', () => updateStarsInput());

function highlightStarsInput(rating) {
    starsInput.forEach((star, index) => {
        star.style.color = index < rating ? '#FFB300' : '#ddd';
    });
}

function updateStarsInput() {
    starsInput.forEach((star, index) => {
        star.classList.toggle('active', index < userRating);
    });
}

function submitComment(event) {
    event.preventDefault();
    const text = document.getElementById('commentTextarea').value.trim();

    if (userRating === 0) {
        alert('Lütfen bir puan seçin.');
        return;
    }
    if (text.length === 0) {
        alert('Lütfen bir yorum yazın.');
        return;
    }

    console.log('Yeni Yorum:', { rating: userRating, text });

    alert('Yorumunuz eklendi! (Demo)');
    document.getElementById('commentTextarea').value = '';
    userRating = 0;
    updateStarsInput();
    selectedRatingText.textContent = 'Puanlamak için yıldızlara tıklayın';
}

function toggleLike(button) {
    button.classList.toggle('liked');
    const span = button.querySelector('span');
    const current = parseInt(span.textContent.match(/\d+/)[0]);
    span.textContent = `${button.classList.contains('liked') ? current + 1 : current - 1} Beğeni`;
}

function showOptions(button) {
    alert('Düzenle / Sil seçenekleri burada görünecek.');
}

function sortComments(sortBy) {
    console.log('Sıralama:', sortBy);
}

function loadMoreComments() {
    console.log('Daha fazla yorum yükleniyor...');
}
