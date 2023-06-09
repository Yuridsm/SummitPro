﻿import { AbstractSpecification } from "./Contracts/AbstractionSpecification";

export default class FieldIsNotNullSpecification extends AbstractSpecification<string> {
    isSatisfiedBy(item: string): boolean {
        return item != null && item != '' && item != undefined;
    }
}