document.addEventListener("DOMContentLoaded", function() {
    var themeToggleBtn = document.getElementById("themeToggleBtn");
    var themeToggleIcon = document.getElementById("themeToggleIcon");
    
    if (themeToggleBtn && themeToggleIcon) {
        // Function to update icon based on theme
        function updateIcon(theme) {
            if (theme === 'dark') {
                themeToggleIcon.classList.remove('fa-moon', 'far');
                themeToggleIcon.classList.add('fa-sun', 'fas');
                themeToggleIcon.style.color = '#f59e0b'; // golden sun color
            } else {
                themeToggleIcon.classList.remove('fa-sun', 'fas');
                themeToggleIcon.classList.add('fa-moon', 'far');
                themeToggleIcon.style.color = 'var(--gray-500)';
            }
        }
        
        // Set initial icon state based on active attribute
        var currentTheme = document.documentElement.getAttribute('data-theme') || 'light';
        updateIcon(currentTheme);
        
        // Toggle theme on button click
        themeToggleBtn.addEventListener("click", function() {
            var theme = document.documentElement.getAttribute('data-theme') === 'dark' ? 'light' : 'dark';
            document.documentElement.setAttribute('data-theme', theme);
            localStorage.setItem('theme', theme);
            updateIcon(theme);
            
            // Dispatch a custom event in case charts or other scripts need to re-render
            window.dispatchEvent(new Event('themeChanged'));
        });
    }
});
