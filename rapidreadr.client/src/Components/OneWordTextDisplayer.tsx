import { useEffect, useState } from "react";

interface OneWordTextDisplayerProps {
    pdfId: string | undefined;
    speed: number | undefined;
}

const OneWordTextDisplayer: React.FC<OneWordTextDisplayerProps> = ({ pdfId, speed }) => {
    const [text, setText] = useState<string>("");
    const [wordToDisplay, setWordToDisplay] = useState<string>("");
    const [splitText, setSplitText] = useState<string[]>([]);
    const [showButton, setShowButton] = useState<string>("none");


    let currentIndex = 0;
    let intervalId: NodeJS.Timeout | null = null;

    useEffect(() => {
        fetch(`https://localhost:7214/api/Pdf/${pdfId}`)
            .then((response) => response.text())
            .then((data) => setText(data))
            .catch((error) => console.error(error));
    }, [pdfId])

    useEffect(() => {
        setSplitText(text.split(/\s+/));
        setShowButton("block")
    }, [text])

    const HandleText = () => {

        if (intervalId) {
            clearInterval(intervalId);
        }

        currentIndex = 0; 

        intervalId = setInterval(() => {
            if (currentIndex < splitText.length) {
                setWordToDisplay(splitText[currentIndex]);
                currentIndex++;
            } else {
                clearInterval(intervalId!);
            }
        }, speed);
    }

    return (
        <div>
            <a>{wordToDisplay}</a>
            <button style={{ display: showButton }} onClick={HandleText}>Start</button>
        </div>
    )
}

export default OneWordTextDisplayer;