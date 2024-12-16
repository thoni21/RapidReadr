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
    const [isPlaying, setIsPlaying] = useState(false);


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

        setIsPlaying(!isPlaying);

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
            <h1>{wordToDisplay}</h1>
            <div className="d-flex justify-content-center align-items-center my-3">
                <button className="btn btn-primary rounded-circle mx-2">
                    <i className="bi bi-arrow-left"></i>
                </button>

                <button
                    className="btn btn-primary rounded-circle mx-2"
                    onClick={HandleText}
                >
                    <i className={`bi ${isPlaying ? "bi-pause-fill" : "bi-play-fill"}`}></i>
                </button>

                <button className="btn btn-primary rounded-circle mx-2">
                    <i className="bi bi-arrow-right"></i>
                </button>
            </div>
        </div>
    )
}

export default OneWordTextDisplayer;