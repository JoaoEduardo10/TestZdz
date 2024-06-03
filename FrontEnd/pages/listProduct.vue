<template>
  <TableList :itemType="itemType" :title="title" />
</template>

<script lang="ts">
export default {
  name: "ListProductPage",
  data() {
    return {
      itemType: "product",
      title: "Listagem dos produtos",
    };
  },
};
</script>
