import { useEffect, useState } from "react";

interface OneWordTextDisplayerProps {
    pdfId: string | undefined; // Define the prop type (assuming pdfId is a string)
}

const OneWordTextDisplayer: React.FC<OneWordTextDisplayerProps> = ({ pdfId }) => {
    const [text, setText] = useState<string>("");
    const [wordToDisplay, setWordToDisplay] = useState<string>("");

    useEffect(() => {
        fetch(`https://localhost:7214/api/Pdf/${pdfId}`)
            .then((response) => response.text())
            .then((data) => setText(data))
            .catch((error) => console.error(error));
    })


    return (
        <div>
            <a>{wordToDisplay}</a>
            <p>{text}</p>
        </div>
    )
}

export default OneWordTextDisplayer;