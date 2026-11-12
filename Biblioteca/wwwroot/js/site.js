
const sidebarToggle = document.getElementById("sidebarToggle");
const sidebar = document.getElementById("sidebar");
const backdrop = document.getElementById("sidebarBackdrop");

if (sidebarToggle && sidebar && backdrop) {

    sidebarToggle.addEventListener("click", () => {
        sidebar.classList.toggle("open");
        backdrop.classList.toggle("show");
    });

    backdrop.addEventListener("click", () => {
        sidebar.classList.remove("open");
        backdrop.classList.remove("show");
    });
}
