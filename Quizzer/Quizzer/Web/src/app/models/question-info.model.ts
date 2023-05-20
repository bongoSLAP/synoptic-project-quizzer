import { AnswerInfo } from "./answer-info.model";

export class QuestionInfo {
    public id: string;
    public text: string;
    public questionIndex: number;
    public answers: AnswerInfo[];
}
