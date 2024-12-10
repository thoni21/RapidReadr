import { useState } from "react";

function FileUploader() {
    const [file, setFile] = useState<File | null>(null);

    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.files) {
            setFile(e.target.files[0]);
        }
    };

    const handleUpload = async () => {
        if (file) {
            console.log('Uploading file...');

            const formData = new FormData();
            formData.append('file', file);

            try {
                const result = await fetch('https://localhost:7214/api/Pdf/upload', {
                    method: 'POST',
                    credentials: 'include',
                    body: formData,
                });

                const data = await result.json();

                console.log(data);
            } catch (error) {
                console.error(error);
            }
        }
    }

    return (
        <div>
            <div>
                <input type="file" onChange={handleFileChange} accept="application/pdf" />
            </div>

            {file && (
                <button
                    onClick={handleUpload}
                    className="submit"
                >Upload a file</button>
            )}
        </div>
    );
}

export default FileUploader;