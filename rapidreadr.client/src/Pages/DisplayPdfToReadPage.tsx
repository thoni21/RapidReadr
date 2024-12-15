import { useParams } from "react-router-dom";
import AuthorizeView from "../Components/AuthorizeView";
import OneWordTextDisplayer from "../Components/OneWordTextDisplayer";
import { useState } from "react";

function DisplayPdfToReadPage() {
    const { id } = useParams<{ id: string }>();
    const [speedToRead, setSpeedToRead] = useState<number | undefined>();
    const [ wordsPerMinute, setWordsPerMinute ] = useState<number | "">(0);
    const [isSubmitted, setIsSubmitted] = useState<boolean>(false);

    const WordsPerMinuteToMiliseconds = (e: React.FormEvent) => {
        e.preventDefault();
        setSpeedToRead((60000 / Number(wordsPerMinute))); // WPM to milisecond pause between each word
        setIsSubmitted(true); 
    };

    return (
        <AuthorizeView>
            {isSubmitted ? (
                <OneWordTextDisplayer pdfId={id} speed={speedToRead} />
            ) : (
                <form onSubmit={WordsPerMinuteToMiliseconds}>
                    <input
                        type="number"
                        value={wordsPerMinute === "" ? "" : wordsPerMinute}
                        onChange={(e) => setWordsPerMinute(Number(e.target.value) || "")}
                        placeholder="Enter Words Per Minute"
                    />
                    <button type="submit">Go</button>
                </form>
            )}
        </AuthorizeView>
    );
}

export default DisplayPdfToReadPage;