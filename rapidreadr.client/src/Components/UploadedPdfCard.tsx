import { useNavigate } from "react-router-dom";

interface UploadedPdfCardProps {
    pdfId: string;
    pdfName: string;
    onDelete: (pdfId: string) => void;
}

const UploadedPdfCard: React.FC<UploadedPdfCardProps> = ({ pdfId, pdfName, onDelete }) => {
    const navigate = useNavigate();

    const handlePick = (e: React.FormEvent) => {
        e.preventDefault();
        if (pdfId) {
            navigate(`/display/${pdfId}`);
        }
    };

    const handleDelete = async () => {
        try {
            const response = await fetch(`https://localhost:7214/api/ActivelyReading/${pdfId}`, {
                method: 'DELETE',
                credentials: 'include', // keep credentials for authentication if needed
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            if (response.ok) {
                // Call the onDelete callback to update the parent state
                onDelete(pdfId);
            } else {
                console.error('Failed to delete PDF');
            }
        } catch (error) {
            console.error('Error deleting PDF:', error);
        }
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
};

export default UploadedPdfCard;
