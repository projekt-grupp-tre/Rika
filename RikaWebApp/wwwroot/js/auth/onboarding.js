document.addEventListener("DOMContentLoaded", function () {
    
    if (window.location.href.includes("/onboarding")) {
        onboardingFunction();
    }
});

function onboardingFunction() {
    const slides = document.querySelectorAll('.container .content');
    let currentSlideIndex = 0;

    if (slides.length > 0) {
        slides[currentSlideIndex].style.display = 'block';

        const buttons = document.querySelectorAll('#onboard-btn');

        if (buttons.length > 0) {
            buttons.forEach(button => {
                button.addEventListener('click', () => {
                    slides[currentSlideIndex].style.display = 'none';
                    currentSlideIndex++;

                    if (currentSlideIndex < slides.length) {
                        slides[currentSlideIndex].style.display = 'block';
                    } else {
                        window.location.href = "https://localhost:7259/SignUp/SignUpView";
                    }
                });
            });
        }
    }
}
