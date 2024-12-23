using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Intrefaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CommentExists(int id)
        {
            return await _context.Comments.AnyAsync(comment => comment.Id == id);
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            {
                var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

                if (commentModel == null)
                {
                    return null;
                }

                _context.Comments.Remove(commentModel);
                await _context.SaveChangesAsync();
                return commentModel;
            }
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.Include(c => c.AppUser).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.Include(c => c.AppUser).FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null) return null;
            return comment;
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentData)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = commentData.Title;
            existingComment.Content = commentData.Content;

            await _context.SaveChangesAsync();

            return existingComment;
        }
    }
}