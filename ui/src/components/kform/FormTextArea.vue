<template>
    <fieldwrapper>
      <klabel :editor-id="id" :editor-valid="valid">
        {{ label }}
      </klabel>
      <div class="k-form-field-wrap">
        <span class="k-textarea">
          <ktextarea 
            :valid="valid"
            :value="value"
            :id="id"
            :maxlength="max"
            @input="handleChange"
            @blur="handleBlur"
            @focus="handleFocus"
            :icon-name="iconName"
            :rows="rows"
          ></ktextarea>
        </span>
        <error v-if="showValidationMessage">
          {{ validationMessage }}
          <hint :direction="'end'"> {{ value.length }} / {{ max }} </hint>
        </error>
        <div v-else style="display: flex; justify-content: space-between">
          <hint>{{ hint }}</hint>
          <hint :direction="'end'"> {{ value.length }} / {{ max }} </hint>
        </div>
      </div>
    </fieldwrapper>
  </template>
  <script>
  import { FieldWrapper } from '@progress/kendo-vue-form';
  import { Error, Hint, Label } from '@progress/kendo-vue-labels';
  import { TextArea } from '@progress/kendo-vue-inputs';
  
  export default {
    props: {
      touched: Boolean,
      label: String,
      validationMessage: String,
      hint: String,
      id: String,
      max: Number,
      valid: Boolean,
      value: {
        type: String,
        default: '',
      },
      iconName: String,
      rows: {
        type: Number,
        default: 3
      }
    },
    components: {
      ktextarea: TextArea,
      fieldwrapper: FieldWrapper,
      error: Error,
      hint: Hint,
      klabel: Label,
    },
    emits: {
      change: null,
      blur: null,
      focus: null,
    },
    computed: {
      showValidationMessage() {
        return this.$props.touched && this.$props.validationMessage;
      },
      showHint() {
        return !this.showValidationMessage && this.$props.hint;
      },
      hintId() {
        return this.showHint ? `${this.$props.id}_hint` : '';
      },
      errorId() {
        return this.showValidationMessage ? `${this.$props.id}_error` : '';
      },
    },
    methods: {
      handleChange(e) {
        this.$emit('change', e);
      },
      handleBlur(e) {
        this.$emit('blur', e);
      },
      handleFocus(e) {
        this.$emit('focus', e);
      },
    },
  };
  </script>