<template><Product :isEditing="isEditing" /></template>

<script lang="ts">
export default {
  name: "ProductByIdPage",
  data() {
    return {
      isEditing: true,
    };
  },
};
</script>
