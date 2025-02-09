// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

        const SELECTOR_SIDEBAR_WRAPPER = '.sidebar-wrapper';
        const Default = {
          scrollbarTheme: 'os-theme-light',
          scrollbarAutoHide: 'leave',
          scrollbarClickScroll: true,
        };
        document.addEventListener('DOMContentLoaded', function () {
          const sidebarWrapper = document.querySelector(SELECTOR_SIDEBAR_WRAPPER);
          if (sidebarWrapper && typeof OverlayScrollbarsGlobal?.OverlayScrollbars !== 'undefined') {
            OverlayScrollbarsGlobal.OverlayScrollbars(sidebarWrapper, {
              scrollbars: {
                theme: Default.scrollbarTheme,
                autoHide: Default.scrollbarAutoHide,
                clickScroll: Default.scrollbarClickScroll,
              },
            });
          }
        });
        document.addEventListener('DOMContentLoaded', () => {
          const appSidebar = document.querySelector('.app-sidebar');
        const sidebarColorModes = document.querySelector('#sidebar-color-modes');
        const sidebarColor = document.querySelector('#sidebar-color');
        const sidebarColorCode = document.querySelector('#sidebar-color-code');

        const themeBg = [
        'bg-primary',
        'bg-primary-subtle',
        'bg-secondary',
        'bg-secondary-subtle',
        'bg-success',
        'bg-success-subtle',
        'bg-danger',
        'bg-danger-subtle',
        'bg-warning',
        'bg-warning-subtle',
        'bg-info',
        'bg-info-subtle',
        'bg-light',
        'bg-light-subtle',
        'bg-dark',
        'bg-dark-subtle',
        'bg-body-secondary',
        'bg-body-tertiary',
        'bg-body',
        'bg-black',
        'bg-white',
        'bg-transparent',
        ];

          // loop through each option themeBg array
          document.querySelector('#sidebar-color').innerHTML = themeBg.map((bg) => {
            // return option element with value and text
            return `<option value="${bg}" class="text-${bg}">${bg}</option>`;
          });

        let sidebarColorMode = '';
        let sidebarBg = '';

        function updateSidebar() {
            appSidebar.setAttribute('data-bs-theme', sidebarColorMode);

        sidebarColorCode.innerHTML = `<pre><code class="language-html">&lt;aside class="app-sidebar ${sidebarBg}" data-bs-theme="${sidebarColorMode}"&gt;...&lt;/aside&gt;</code></pre>`;
          }

          sidebarColorModes.addEventListener('input', (event) => {
            sidebarColorMode = event.target.value;
        updateSidebar();
          });

          sidebarColor.addEventListener('input', (event) => {
            sidebarBg = event.target.value;

            themeBg.forEach((className) => {
            appSidebar.classList.remove(className);
            });

        if (themeBg.includes(sidebarBg)) {
            appSidebar.classList.add(sidebarBg);
            }

        updateSidebar();
          });
        });

        document.addEventListener('DOMContentLoaded', () => {
          const appNavbar = document.querySelector('.app-header');
        const navbarColorModes = document.querySelector('#navbar-color-modes');
        const navbarColor = document.querySelector('#navbar-color');
        const navbarColorCode = document.querySelector('#navbar-color-code');

        const themeBg = [
        'bg-primary',
        'bg-primary-subtle',
        'bg-secondary',
        'bg-secondary-subtle',
        'bg-success',
        'bg-success-subtle',
        'bg-danger',
        'bg-danger-subtle',
        'bg-warning',
        'bg-warning-subtle',
        'bg-info',
        'bg-info-subtle',
        'bg-light',
        'bg-light-subtle',
        'bg-dark',
        'bg-dark-subtle',
        'bg-body-secondary',
        'bg-body-tertiary',
        'bg-body',
        'bg-black',
        'bg-white',
        'bg-transparent',
        ];

          // loop through each option themeBg array
          document.querySelector('#navbar-color').innerHTML = themeBg.map((bg) => {
            // return option element with value and text
            return `<option value="${bg}" class="text-${bg}">${bg}</option>`;
          });

        let navbarColorMode = '';
        let navbarBg = '';

        function updateNavbar() {
            appNavbar.setAttribute('data-bs-theme', navbarColorMode);
        navbarColorCode.innerHTML = `<pre><code class="language-html">&lt;nav class="app-header navbar navbar-expand ${navbarBg}" data-bs-theme="${navbarColorMode}"&gt;...&lt;/nav&gt;</code></pre>`;
          }

          navbarColorModes.addEventListener('input', (event) => {
            navbarColorMode = event.target.value;
            updateNavbar();
          });

          navbarColor.addEventListener('input', (event) => {
            navbarBg = event.target.value;

            themeBg.forEach((className) => {
              appNavbar.classList.remove(className);
            });

            if (themeBg.includes(navbarBg)) {
              appNavbar.classList.add(navbarBg);
            }

            updateNavbar();
          });
        });
