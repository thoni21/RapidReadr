import { useEffect, useRef, useState } from "react";

interface OneWordTextDisplayerProps {
    pdfId: string | undefined;
    speed: number | undefined;
}

const OneWordTextDisplayer: React.FC<OneWordTextDisplayerProps> = ({ pdfId, speed }) => {
    const [text, setText] = useState<string>("");
    const [wordToDisplay, setWordToDisplay] = useState<string>("");
    const [splitText, setSplitText] = useState<string[]>([]);
    const [isPlaying, setIsPlaying] = useState<boolean>(false);

    const currentIndexRef = useRef<number>(0);
    const intervalIdRef = useRef<NodeJS.Timeout | null>(null);

    useEffect(() => {
        fetch(`https://localhost:7214/api/Pdf/${pdfId}`)
            .then((response) => response.text())
            .then((data) => setText(data))
            .catch((error) => console.error(error));
    }, [pdfId])

    useEffect(() => {
        setSplitText(text.split(/\s+/));
    }, [text])

    const HandleText = () => {
        setIsPlaying((prevIsPlaying) => {
            if (prevIsPlaying) {
                // Pause logic
                if (intervalIdRef.current) {
                    clearInterval(intervalIdRef.current);
                    intervalIdRef.current = null;
                }
                return false;
            } 

            // Play logic
            if (!intervalIdRef.current) {
                intervalIdRef.current = setInterval(() => {
                    if (currentIndexRef.current < splitText.length) {
                        setWordToDisplay(splitText[currentIndexRef.current]);
                        currentIndexRef.current += 1;
                    } else {
                        clearInterval(intervalIdRef.current!);
                        intervalIdRef.current = null;
                    }
                }, speed);
            }
            return true;
        });
    };

    const ManuallyIterateThroughText = (stepSize: number) => {
        currentIndexRef.current += stepSize;
        setWordToDisplay(splitText[currentIndexRef.current-1]);
    };

    return (
        <div>
            <h1>{wordToDisplay}</h1>
            <div className="d-flex justify-content-center align-items-center my-3">
                <button
                    className="btn btn-primary rounded-circle mx-2"
                    onClick={() => { ManuallyIterateThroughText(-1) }}
                >
                    <i className="bi bi-arrow-left"></i>
                </button>

                <button
                    className="btn btn-primary rounded-circle mx-2"
                    onClick={HandleText}
                >
                    <i className={`bi ${isPlaying ? "bi-pause-fill" : "bi-play-fill"}`}></i>
                </button>

                <button
                    className="btn btn-primary rounded-circle mx-2"
                    onClick={() => { ManuallyIterateThroughText(1) }}
                >
                    <i className="bi bi-arrow-right"></i>
                </button>
            </div>
        </div>
    )
}

export default OneWordTextDisplayer;