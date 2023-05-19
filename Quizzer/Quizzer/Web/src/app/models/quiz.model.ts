import { Question } from "./question.model";

export class Quiz {
    public Id: string;
    public Title: string;
    public Description: string;
    public Questions: Question[]
}
