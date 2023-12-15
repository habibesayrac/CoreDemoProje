using BusinessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        EfCategoryRepository _efCategoryRepository;

        public CategoryManager(EfCategoryRepository efCategoryRepository)
        {
            _efCategoryRepository = efCategoryRepository;
        }

        public void CategoryAdd(Category category)
        {
          _efCategoryRepository.Insert(category);


        }

        public void CategoryDelete(Category category)
        {
            _efCategoryRepository.Delete(category);

        }

        public void CategoryUpdate(Category category)
        {
            _efCategoryRepository.Update(category);

        }

        public Category GetById(int id)
        {
           return _efCategoryRepository.GetByID(id);

        }

        public List<Category> GetList()
        {
            return _efCategoryRepository.GetListAll();
        }
    }
}
