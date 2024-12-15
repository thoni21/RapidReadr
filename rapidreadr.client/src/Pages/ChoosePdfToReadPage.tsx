import AuthorizeView from "../Components/AuthorizeView";
import { useEffect, useState } from "react";
import UploadedPdfCard from "../Components/UploadedPdfCard";

interface ActivelyReading {
    id: string;
    userId: string;
    path: string;
    dateUploaded: Date;
    timestamp: number;
}

function ChoosePdfToReadPage() {
    const [activelyReadingArray, setActivelyReadingArray] = useState<ActivelyReading[]>([]);

    useEffect(() => {
        fetch(`https://localhost:7214/api/ActivelyReading/user`, {
            method: 'GET',
            credentials: 'include',
        })
        .then((response) => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then((data) => {
            setActivelyReadingArray(data);
        })
        .catch((error) => {
            console.error('Error fetching actively reading data:', error);
        });
    })

    return (
        <AuthorizeView>
            {activelyReadingArray.map((item) => (
                <UploadedPdfCard pdfId={item.id} pdfName={item.path} />
            ))}
        </AuthorizeView>
    )
}

export default ChoosePdfToReadPage;