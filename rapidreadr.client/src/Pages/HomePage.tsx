import LogoutLink from "../Components/LogoutLink.tsx";
import AuthorizeView, { AuthorizedUser } from "../Components/AuthorizeView.tsx";
import FileUploader from "../Components/FileUploader.tsx";
function HomePage() {
    return (
        <AuthorizeView>           
            <div className="d-flex flex-column">
                <div className="p-3 mb-2">
                    <FileUploader />
                </div>
                <div className="p-3 mb-2">
                    <a href="/choose">
                        <button>Choose Pdf Page</button>
                    </a>
                </div>
                <div className="p-3 mb-2">
                    <span><LogoutLink>Logout <AuthorizedUser value="email" /></LogoutLink></span>
                </div>
            </div>
        </AuthorizeView>
    );
}
export default HomePage;