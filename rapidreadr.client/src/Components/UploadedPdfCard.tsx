import { useNavigate } from "react-router-dom";

interface UploadedPdfCardProps {
    pdfId: string | undefined;
    pdfName: string | undefined;
}
const UploadedPdfCard: React.FC<UploadedPdfCardProps> = ({ pdfId, pdfName }) => {
    const navigate = useNavigate();

    const handlePick = (e: React.FormEvent) => {
        e.preventDefault();
        if (pdfId) {
            navigate(`/display/${pdfId}`);
        }
    };

    const handleDelete = async () => {
        await fetch(`https://localhost:7214/api/ActivelyReading`, {
            method: 'DELETE',
            credentials: 'include',
            body: pdfId,
        });
    };

    return (
        <div className="card p-4 shadow-sm mt-2 mb-2">
            <div className="card-body">
                <div className="row g-3">

                    <div className="col">
                            <a>{pdfName}</a>
                    </div>
                    <div className="col">
                            <button className="btn btn-primary" onClick={handlePick}>Choose</button>
                    </div>
                    <div className="col">
                            <button className="btn btn-danger" onClick={handleDelete}>Delete</button>
                    </div>

                </div>
            </div>
        </div>
    );

}

export default UploadedPdfCard;