<template>
  <Product :isEditing="isEditing" />
</template>

<script>
export default {
  name: "ProductPage",
  data() {
    return {
      isEditing: false,
    };
  },
};
</script>
