import { Question } from "./question.model";

export class Answer {
    public id: string;
    public text: string;
    public answerIndex: number;
    public questionId: string;
    public question: Question; 
}
