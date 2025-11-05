function switchTab(tabName, event) {
    // Tüm panelleri gizle
    document.querySelectorAll('.tab-pane').forEach(pane => pane.classList.remove('active'));

    // Tüm sekme başlıklarından "active"i kaldır
    document.querySelectorAll('.tab-header').forEach(header => header.classList.remove('active'));

    // Seçili paneli göster ve tıklanan sekmeyi aktif yap
    document.getElementById(tabName).classList.add('active');
    event.target.classList.add('active');
}

function editProfile() {
    alert('Profil düzenleme sayfasına yönlendiriliyorsunuz...');
}

function createList() {
    alert('Yeni liste oluşturma sayfasına yönlendiriliyorsunuz...');
}

function toggleFollow(event) {
    const btn = event.target;
    const isFollowing = btn.classList.contains('btn-following');

    if (isFollowing) {
        btn.classList.remove('btn-following');
        btn.classList.add('btn-follow');
        btn.textContent = '➕ Takip Et';
    } else {
        btn.classList.remove('btn-follow');
        btn.classList.add('btn-following');
        btn.textContent = '✓ Takip Ediliyor';
    }
}
