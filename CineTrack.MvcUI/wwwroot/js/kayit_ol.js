const form = document.getElementById('registerForm');
const username = document.getElementById('username');
const email = document.getElementById('email');
const password = document.getElementById('password');
const passwordConfirm = document.getElementById('passwordConfirm');
const passwordStrength = document.getElementById('passwordStrength');

// Gerçek zamanlı validasyon
username.addEventListener('input', validateUsername);
email.addEventListener('input', validateEmail);
password.addEventListener('input', function() {
    validatePassword();
    checkPasswordStrength();
});
passwordConfirm.addEventListener('input', validatePasswordConfirm);

function validateUsername() {
    const value = username.value.trim();
    const error = document.getElementById('usernameError');
    
    if (value.length < 3) {
        username.classList.add('error');
        username.classList.remove('success');
        error.classList.add('show');
        return false;
    } else {
        username.classList.remove('error');
        username.classList.add('success');
        error.classList.remove('show');
        return true;
    }
}

function validateEmail() {
    const value = email.value.trim();
    const error = document.getElementById('emailError');
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!regex.test(value)) {
        email.classList.add('error');
        email.classList.remove('success');
        error.classList.add('show');
        return false;
    } else {
        email.classList.remove('error');
        email.classList.add('success');
        error.classList.remove('show');
        return true;
    }
}

function validatePassword() {
    const value = password.value;
    
    if (value.length < 6) {
        password.classList.add('error');
        password.classList.remove('success');
        return false;
    } else {
        password.classList.remove('error');
        password.classList.add('success');
        return true;
    }
}

function validatePasswordConfirm() {
    const error = document.getElementById('passwordConfirmError');
    
    if (password.value !== passwordConfirm.value) {
        passwordConfirm.classList.add('error');
        passwordConfirm.classList.remove('success');
        error.classList.add('show');
        return false;
    } else {
        passwordConfirm.classList.remove('error');
        passwordConfirm.classList.add('success');
        error.classList.remove('show');
        return true;
    }
}

function checkPasswordStrength() {
    const value = password.value;
    let strength = 0;
    
    if (value.length >= 8) strength++;
    if (value.match(/[a-z]/) && value.match(/[A-Z]/)) strength++;
    if (value.match(/[0-9]/)) strength++;
    if (value.match(/[^a-zA-Z0-9]/)) strength++;
    
    passwordStrength.className = 'password-strength';
    const strengthText = passwordStrength.querySelector('.strength-text');
    
    if (strength <= 1) {
        passwordStrength.classList.add('strength-weak');
        strengthText.textContent = 'Zayıf şifre';
    } else if (strength <= 2) {
        passwordStrength.classList.add('strength-medium');
        strengthText.textContent = 'Orta güçlü şifre';
    } else {
        passwordStrength.classList.add('strength-strong');
        strengthText.textContent = 'Güçlü şifre';
    }
}

form.addEventListener('submit', function(e) {
    e.preventDefault();
    
    const isUsernameValid = validateUsername();
    const isEmailValid = validateEmail();
    const isPasswordValid = validatePassword();
    const isPasswordConfirmValid = validatePasswordConfirm();
    
    if (isUsernameValid && isEmailValid && isPasswordValid && isPasswordConfirmValid) {
        const formData = {
            username: username.value,
            email: email.value,
            password: password.value
        };

        console.log('Register data:', formData);
        alert('Kayıt başarılı! (Demo mod)');
    }
});
