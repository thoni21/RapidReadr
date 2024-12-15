import LogoutLink from "../Components/LogoutLink.tsx";
import AuthorizeView, { AuthorizedUser } from "../Components/AuthorizeView.tsx";
import FileUploader from "../Components/FileUploader.tsx";
function HomePage() {
    return (
        <AuthorizeView>
            <FileUploader />
            <a href="/choose">
                <button>Choose Pdf Page</button>
            </a>
            <span><LogoutLink>Logout <AuthorizedUser value="email" /></LogoutLink></span>
        </AuthorizeView>
    );
}
export default HomePage;