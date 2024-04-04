<template>
  <label class="m-label">{{ label }}</label>
  <div class="list-gender">
    <div
      class="gender-customer"
      v-for="(gender, index) in genders"
      :key="index"
    >
      <div v-if="index === 0">
        <input
          checked
          type="radio"
          class="input-radio"
          name="gender"
          v-bind:value="gender.value"
          v-model="genderCustomer"
          @click="selectedGender(gender.value)"
        />
      </div>
      <div v-else>
        <input
          type="radio"
          class="input-radio"
          name="gender"
          v-bind:value="gender.value"
          v-model="genderCustomer"
          @click="selectedGender(gender.value)"
        />
      </div>
      <p>{{ gender.text }}</p>
    </div>
  </div>
</template>
<script>
export default {
  name: "MCheckBox",
  props: {
    label: String,
    modelValue: String,
  },
  created() {
    this.genderCustomer = this.modelValue;
  },
  data() {
    return {
      genderCustomer: 0,
      genders: [
        {
          value: 0,
          text: "Nam",
        },
        {
          value: 1,
          text: "Nữ",
        },
        {
          value: 2,
          text: "Khác",
        },
      ],
    };
  },
  methods: {
    /**
     *Cập nhật lại value
     * @param {*Giá trị lựa chọn} option
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    selectedGender(option) {
      this.$emit("update:modelValue", option);
    },
  },
};
</script>
<style scoped>
.list-gender,
.gender-customer {
  display: flex;
}
.m-label {
  margin-top: 20px;
}
</style>
