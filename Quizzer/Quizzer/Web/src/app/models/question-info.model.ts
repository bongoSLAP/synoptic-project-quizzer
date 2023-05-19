import { Answer } from "./answer.model";

export class QuestionInfo {
    public Id: string;
    public Text: string;
    public QuestionIndex: number;
    public Answers: Answer[];
}
