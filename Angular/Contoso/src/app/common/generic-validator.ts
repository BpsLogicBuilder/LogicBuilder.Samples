import { FormGroup } from '@angular/forms';

export class GenericValidator {
    constructor(private validationMessages: { [key: string]: { [key: string]: string } }) {}

    processMessages(container: FormGroup): { [key: string]: string } {
        let messages = {};
        for (let controlKey in container.controls) {
            if (container.controls.hasOwnProperty(controlKey)) {
                let c = container.controls[controlKey];
                // If it is a FormGroup, process its child controls.
                if (c instanceof FormGroup) {
                    let childMessages = this.processMessages(c);
                    Object.assign(messages, childMessages);
                } else {
                    // Only validate if there are validation messages for the control
                    if (this.validationMessages[controlKey]) {
                        messages[controlKey] = '';
                        if ((c.dirty || c.touched) && c.errors) {
                            Object.keys(c.errors).map(messageKey => {
                                if (this.validationMessages[controlKey][messageKey]) {
                                    messages[controlKey] += this.validationMessages[controlKey][messageKey] + ' ';
                                }
                            });
                        }
                    }
                }
            }
        }
        return messages;
    }

    processMessages2(container: FormGroup, parentKey: string): { [key: string]: string } {
        let messages = {};
        for (let controlKey in container.controls) {
            if (container.controls.hasOwnProperty(controlKey)) {
                let c = container.controls[controlKey];
                // If it is a FormGroup, process its child controls.
                if (c instanceof FormGroup) {
                    let childMessages = this.processMessages2(c, controlKey);
                    Object.assign(messages, childMessages);
                } else {
                    // Only validate if there are validation messages for the control
                    let validationKey: string = parentKey ? parentKey.concat('.', controlKey) : controlKey;
                    if (this.validationMessages[validationKey]) {
                        messages[controlKey] = '';
                        if ((c.dirty || c.touched) && c.errors) {
                            Object.keys(c.errors).map(messageKey => {
                                if (this.validationMessages[validationKey][messageKey]) {
                                    messages[controlKey] += this.validationMessages[validationKey][messageKey] + ' ';
                                }
                            });
                        }
                    }
                }
            }
        }
        return messages;
    }

    getErrorCount(container: FormGroup): number {
        let errorCount = 0;
        for (let controlKey in container.controls) {
            if (container.controls.hasOwnProperty(controlKey)) {
                if (container.controls[controlKey].errors) {
                    errorCount += Object.keys(container.controls[controlKey].errors).length;
                    console.log(errorCount);
                }
            }
        }
        return errorCount;
    }
}
