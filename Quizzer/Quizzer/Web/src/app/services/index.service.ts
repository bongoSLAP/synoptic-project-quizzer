import { Injectable } from '@angular/core';
import { QuestionInfo } from '../models/question-info.model';
import { AnswerInfo } from '../models/answer-info.model';

@Injectable({
    providedIn: 'root'
})
export class IndexService {

    constructor() { }

    sortQuestionsByIndexes(questions: QuestionInfo[]):  QuestionInfo[] {
        return questions.sort((a, b) => a.questionIndex - b.questionIndex)
    }

    sortAnswersByIndexes(answers: AnswerInfo[]): AnswerInfo[] {
        return answers.sort((a, b) => a.answerIndex - b.answerIndex);
    }

    mapToAlphabet(index: number): string {
        const alphabet = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
        const charIndex = index % 26;
        return alphabet.charAt(charIndex);
    }
}
