import { Question } from "./question.model";

export class Answer {
   public Id: string;
   public Text: string;
   public AnswerIndex: number;
   public QuestionId: string;
   public Question: Question; 
}
