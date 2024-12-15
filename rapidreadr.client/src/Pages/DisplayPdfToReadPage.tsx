import { useParams } from "react-router-dom";
import AuthorizeView from "../Components/AuthorizeView";
import OneWordTextDisplayer from "../Components/OneWordTextDisplayer";

function DisplayPdfToReadPage() {
    const { id } = useParams<{ id: string }>();


    return (
        <AuthorizeView>
            <OneWordTextDisplayer pdfId={id} />
        </AuthorizeView>
    )
}

export default DisplayPdfToReadPage;