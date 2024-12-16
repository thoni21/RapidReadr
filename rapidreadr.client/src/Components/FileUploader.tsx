import { useState } from "react";

function FileUploader() {
    const [file, setFile] = useState<File | null>(null);
    const [responseText, setResponseText] = useState<string>("");

    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setResponseText("")
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
                const result = await fetch('https://localhost:7214/api/Pdf', {
                    method: 'POST',
                    credentials: 'include',
                    body: formData,
                });

                const data = await result.json();

                console.log(data);
                setResponseText("File uploaded successfully!")
            } catch (error) {
                console.error(error);
                setResponseText("Error uploading file :(")
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
                    className="submit mt-2"
                >Upload a file</button>
            )}

            <div>
                <a>{responseText}</a>
            </div>
        </div>
    );
}

export default FileUploader;