import { useNavigate } from "react-router-dom";
import AuthorizeView from "../Components/AuthorizeView";
import { useState } from "react";

function ChoosePdfToReadPage() {

    const [pdfId, setId] = useState<number | "">("");
    const navigate = useNavigate();

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault(); // Prevents page reload
        if (pdfId) {
            navigate(`/display/${pdfId}`); // Navigate to the details page with the ID
        }
    };

    return (
        <AuthorizeView>
            <form onSubmit={handleSubmit}>
                <input
                    type="number"
                    value={pdfId === "" ? "" : pdfId}
                    onChange={(e) => setId(Number(e.target.value) || "")}
                    placeholder="Enter numeric ID"
                />
                <button type="submit">Go</button>
            </form>
        </AuthorizeView>
    )
}

export default ChoosePdfToReadPage;