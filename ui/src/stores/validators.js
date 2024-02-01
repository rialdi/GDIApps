import { getter } from '@progress/kendo-vue-common';

const emailRegex = new RegExp(/\S+@\S+\.\S+/);
const phoneRegex = new RegExp(/^[0-9 ()+-]+$/);
const ccardRegex = new RegExp(/^[0-9-]+$/);
const cvcRegex = new RegExp(/^[0-9]+$/);
const bankAccountNoRegex = new RegExp(/^[0-9]+$/);

export const termsValidator = value =>
  value ? '' : 'Its required to agree with Terms and Conditions.';
export const emailValidator = value =>
  !value
    ? 'Email field is required.'
    : emailRegex.test(value)
    ? ''
    : 'Email is not in a valid format.';
export const nameValidator = value =>
  !value
    ? 'This field is required.'
    : value.length < 3
    ? 'This field should be at least 3 characters long.'
    : '';
export const userNameValidator = value =>
  !value
    ? 'User Name is required'
    : value.length < 5
    ? 'User name should be at least 5 characters long.'
    : '';
export const phoneValidator = value =>
  !value
    ? 'Phone number is required.'
    : phoneRegex.test(value)
    ? ''
    : 'Not a valid phone number.';
export const cardValidator = value =>
  !value
    ? 'Credit card number is required. '
    : ccardRegex.test(value)
    ? ''
    : 'Not a valid credit card number format.';
export const cvcValidator = value =>
  !value
    ? 'CVC code is required,'
    : cvcRegex.test(value) || value.length !== 3
    ? ''
    : 'Not a valid CVC code format.';
export const guestsValidator = value =>
  !value
    ? 'Number of guests is required'
    : value <= 5
    ? ''
    : 'Maximum 5 guests';
  export const bankAccountNoValidator = value =>
    !value
      ? 'This field is required,'
      : bankAccountNoRegex.test(value) && value.length >= 5
      ? ''
      : 'Bank Account No should be at least 5 characters long.';
export const nightsValidator = value =>
  value ? '' : 'Number of Nights is required.';
export const arrivalDateValidator = value =>
  value ? '' : 'Arrival Date is required.';
export const colorValidator = value => (value ? '' : 'Color is required.');
export const requiredValidator = value =>
  value ? '' : 'This field is required.';
export const passwordValidator = value =>
  value && value.length > 7 ? '' : 'Password must be at least 7 symbols.';
export const addressValidator = value => (value ? '' : 'Address is required.');

const userNameGetter = getter('username');
const emailGetter = getter('email');

export const formValidator = values => {
  const userName = userNameGetter(values);
  const emailValue = emailGetter(values);

  if (userName && emailValue && emailRegex.test(emailValue)) {
    return {};
  }

  return {
    VALIDATION_SUMMARY: 'Please fill in the following fields.',
    ['username']: !userName ? 'User Name is required.' : '',
    ['email']:
      emailValue && emailRegex.test(emailValue)
        ? ''
        : 'Email is required and should be in a valid format.',
  };
};

export const formChangePasswordValidator = values => {
  const currPassword = values['currPassword'];
  const newPassword = values['newPassword'];
  const newPasswordConfirm = values['newPasswordConfirm'];
  if(newPassword == newPasswordConfirm) {
    return {};
  }
  return {
    VALIDATION_SUMMARY: 'Please fill in the following fields.',
    ['currPassword']: !currPassword ? 'currPassword is required.' : '',
    ['newPassword']: !newPassword ? 'newPassword is required.' : '',
    ['newPasswordConfirm']: !newPasswordConfirm &&  newPasswordConfirm == newPassword ? '' : 'New Password is not matched',
  };
}

