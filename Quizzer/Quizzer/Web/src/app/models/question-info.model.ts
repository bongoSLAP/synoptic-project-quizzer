import { Answer } from "./answer.model";

export class QuestionInfo {
    public id: string;
    public text: string;
    public questionIndex: number;
    public answers: Answer[];
}
